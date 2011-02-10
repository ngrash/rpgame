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
    public class Map : IMessageChannel
    {
        EntityLayer entityLayer = new EntityLayer();
        CollisionSystem collisionSystem = new CollisionSystem();

        public List<Layer> Layers
        {
            get;
            set;
        }

        internal void Update(float timeElapsed)
        {
            foreach (Entity entity in entityLayer.Entities)
            {
                entity.Update(timeElapsed);
            }
        }

        public static Map Load(StreamReader mapReader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map map = (Map)serializer.Deserialize(mapReader);

            foreach (Layer layer in map.Layers)
            {
                if (layer is EntityLayer)
                {
                    map.entityLayer = (EntityLayer)layer;
                    break;
                }
            }

            return map;
        }

        public static void Save(StreamWriter mapWriter, Map map)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map), new Type[] { typeof(TileLayer), typeof(EntityLayer) });
            serializer.Serialize(mapWriter, map);
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

            Entity player = new Entity()
            {
                Position = new Point(0, 0)
            };
            player.Features.Add(new PlayerCharacterFeature());
            player.Features.Add(new AnimatedCharacterFeature()
            {
                Tileset = new CharacterTileset(new Bitmap(@"C:\Users\Nico\Desktop\res_viewer.png")),
                Offset = new Point(-12, -25)
            });
            player.Features.Add(new MoveFeature());
            player.Features.Add(new CollidableFeature(this.collisionSystem)
            {
                HitBox = new Rectangle(-12, -25, 24, 30)
            });
            player["SPEED"] = 2;
            SpawnEntity(player);
            ProcessMessage(new SetFocusMessage()
            {
                TargetEntity = player
            });

            Entity obstacle = new Entity() {
                Position = new Point(30, 30)
            };
            obstacle.Features.Add(new CollidableFeature(this.collisionSystem)
            {
                HitBox = new Rectangle(0, 0, 10, 10)
            });
            SpawnEntity(obstacle);
        }

        void SpawnEntity(Entity entity)
        {
            this.entityLayer.Entities.Add(entity);
            this.collisionSystem.AddEntity(entity);
            this.MessageIncoming += entity.ProcessMessage;
        }

        void KillEntity(Entity entity)
        {
            this.entityLayer.Entities.Remove(entity);
            this.MessageIncoming -= entity.ProcessMessage;
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
    }
}
