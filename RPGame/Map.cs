using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using RPGame.Tiles;
using RPGame.Messaging;
using RPGame.Features;
using RPGame.Messaging.Messages;
using System.IO;
using System.Xml.Serialization;

namespace RPGame
{
    public class Map : IMessageChannel, IMessageReceiver
    {
        const float MESSAGE_DELAY = 0.2f;
        float lastMessage = 0;

        EntityLayer entityLayer = new EntityLayer();
        CollisionSystem collisionSystem = new CollisionSystem();
        Stack<Entity> entitiesToRemove = new Stack<Entity>();
        Stack<Entity> entitiesToAdd = new Stack<Entity>();
        Queue<Entity> messages = new Queue<Entity>();

        public List<Layer> Layers
        {
            get;
            set;
        }

        internal void Update(float timeElapsed)
        {
            this.collisionSystem.Update(timeElapsed);

            foreach (Entity entity in entityLayer.Entities)
            {
                entity.Update(timeElapsed);
            }

            while (this.entitiesToRemove.Count > 0)
            {
                Entity entityToRemove = this.entitiesToRemove.Pop();
                KillEntity(entityToRemove);
            }

            while (this.entitiesToAdd.Count > 0)
            {
                Entity entityToAdd = this.entitiesToAdd.Pop();
                SpawnEntity(entityToAdd);
            }

            this.lastMessage += timeElapsed;
            if (this.lastMessage >= MESSAGE_DELAY)
            {
                if (this.messages.Count > 0)
                {
                    SpawnEntity(this.messages.Dequeue());
                }

                this.lastMessage = 0;
            }
        }
    
        public Map()
        {
            Layers = new List<Layer>();
        }

        public void Load()
        {
            TileLayer groundLayer = new TileLayer();
            Layers.Add(groundLayer);

            Layers.Add(this.entityLayer);

            // Spieler erzeugen
            Entity player = new Entity()
            {
                Position = new Point(0, 0),
                Name = "player"
            };

            Layers.Add(new HudLayer(player));

            player.Features.Add(new PlayerCharacterFeature());
            player.Features.Add(new AnimatedCharacterFeature()
            {
                Tileset = new CharacterTileset(new Bitmap(@"X:\Develop\Projekte\RPGame\RPGame\bin\Debug\player.png")),
                Offset = new Point(-12, -25),
                Direction = Direction.Down,
                Pose = CharacterTileset.CharacterPoseType.Standing
            });
            player.Features.Add(new MoveFeature());
            player.Features.Add(new CollidableFeature(this.collisionSystem)
            {
                HitBox = new Rectangle(-8, -1, 15, 7)
            });
            player.Features.Add(new AttackFeature(this.collisionSystem));
            player.Features.Add(new LevelFeature()
            {
                Experience = 1,
                Level = 1,
                RequiredExperience = 10
            });
            player["SPEED"] = 2;
            SpawnEntity(player);
            ProcessMessage(new SetFocusMessage()
            {
                TargetEntity = player
            });

            // Gegner erzeugen
            Entity enemy = new Entity()
            {
                Position = new Point(100, 100),
                Name = "enemy"
            };
            enemy.Features.Add(new AnimatedCharacterFeature()
            {
                Tileset = new CharacterTileset(new Bitmap(@"X:\Develop\Projekte\RPGame\RPGame\bin\Debug\enemy.png")),
                Offset = new Point(-12, -25),
                Direction = Direction.Down,
                Pose = CharacterTileset.CharacterPoseType.Standing
            });
            enemy.Features.Add(new CollidableFeature(this.collisionSystem)
            {
                HitBox = new Rectangle(-8, -5, 15, 10)
            });
            enemy.Features.Add(new ExperienceFeature()
            {
                Experience = 30
            });
            enemy.Features.Add(new DestructibleFeature(this.collisionSystem)
            {
                Health = 15,
                HitBox = new Rectangle(-8, -12, 15, 10)
            });
            SpawnEntity(enemy);
        }

        void SpawnEntity(Entity entity)
        {
            this.entityLayer.Entities.Add(entity);
            this.collisionSystem.AddEntity(entity);
            this.MessageIncoming += entity.ProcessMessage;
            entity.MessageIncoming += this.ReceiveMessage;
        }

        void KillEntity(Entity entity)
        {
            this.entityLayer.Entities.Remove(entity);
            this.collisionSystem.RemoveEntity(entity);
            this.MessageIncoming -= entity.ProcessMessage;
            entity.MessageIncoming -= this.ReceiveMessage;
        }

        public Surface GetViewSpace(Point camera, Size viewSize)
        {
            Surface viewSpace = new Surface(viewSize);

            foreach (Layer layer in Layers)
            {
                Surface layerSurface = layer.GetViewSpace(camera, viewSize);
                viewSpace.Blit(layerSurface);
            }

            return viewSpace;
        }

        public event Action<IMessage> MessageIncoming;

        public void ProcessMessage(IMessage message)
        {
            if (MessageIncoming != null)
            {
                MessageIncoming(message);
            }
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is SpawnMessage)
            {
                Entity entityToSpawn = ((SpawnMessage)message).EntityToSpawn;
                this.entitiesToAdd.Push(entityToSpawn);
            }
            else if (message is KillMessage)
            {
                Entity entityToKill = ((KillMessage)message).EntityToKill;
                this.entitiesToRemove.Push(entityToKill);
            }
            else if (message is ExperienceMessage)
            {
                ExperienceMessage expMessage = (ExperienceMessage)message;
                string text = string.Format("Exp: {0}", expMessage.Experience);
                SpawnText(text, Color.Blue, expMessage.Target.Position, 13);
            }
            else if (message is LevelUpMessage)
            {
                LevelUpMessage levelUpMessage = (LevelUpMessage)message;
                string text = string.Format("Level {0}", levelUpMessage.NewLevel);
                SpawnText(text, Color.Yellow, new Point(levelUpMessage.Target.Position.X, levelUpMessage.Target.Position.Y - 2), 15);
            }
        }

        private void SpawnText(string text, Color color, Point position, int fontSize)
        {
            Entity textEntity = new Entity();
            textEntity.Features.Add(new MoveFeature());
            textEntity["DIRECTION"] = Direction.Up;
            textEntity["SPEED"] = 1;
            textEntity.ProcessMessage(new StartMovingMessage());
            textEntity.Position = position;
            textEntity.Features.Add(new DieAfterTimeFeature()
            {
                TimeTillDeath = 1
            });
            textEntity.Features.Add(new SpriteFeature());
            textEntity.Features.Add(new TextFeature()
            {
                Entity = textEntity,
                Color = color,
                Font = new SdlDotNet.Graphics.Font(@"X:\Develop\Projekte\RPGame\RPGame\bin\Debug\arial.ttf", fontSize),
                Text = text
            });
            messages.Enqueue(textEntity);
        }
    }
}
