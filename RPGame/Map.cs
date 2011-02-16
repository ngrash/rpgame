﻿using System;
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
        EntityLayer entityLayer = new EntityLayer();
        CollisionSystem collisionSystem = new CollisionSystem();
        Stack<Entity> entitesToRemove = new Stack<Entity>();

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

            while (this.entitesToRemove.Count > 0)
            {
                Entity entityToRemove = this.entitesToRemove.Pop();
                KillEntity(entityToRemove);
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
            enemy.Features.Add(new DestructibleFeature(this.collisionSystem)
            {
                Health = 100,
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
                SpawnEntity(entityToSpawn);
            }
            else if (message is KillMessage)
            {
                Entity entityToKill = ((KillMessage)message).EntityToKill;
                this.entitesToRemove.Push(entityToKill);
            }
        }
    }
}
