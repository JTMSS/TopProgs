using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace TopProgs.Helpers
{
    // Example of adding an extension to IDatabase.
    ////public static class StackExchangeRedisExtensions
    ////{
    ////    public static T GetIt<T>(this IDatabase cache, string key)
    ////    {
    ////        return default(T);
    ////    }
    ////}

    internal class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions cfgRedis = ConfigurationOptions.Parse("vaxi.redis.cache.windows.net,abortConnect=false,ssl=true,password=Mc5m0xdcoq63ox9+6nyoeBR9qtFpiYb0rG/79wyWo3c=", true);
            return ConnectionMultiplexer.Connect(cfgRedis);
        });

        private static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static bool GetIt<T>(string key, ref T obj)
        {
            IDatabase cache;
            byte[] bytes;
            bool ok = false;

            try
            {
                obj = default(T);

                cache = Connection.GetDatabase();
                if (cache.KeyExists(key))
                {
                    bytes = cache.StringGet(key);
                    ok = DeserializeFromBytes<T>(bytes, ref obj);
                }
            }
            catch
            {
                obj = default(T);
            }

            return ok;
        }

        public static bool DeserializeFromBytes<T>(byte[] bytes, ref T obj)
        {
            bool ok = false;

            try
            {
                obj = default(T);

                if (bytes != null)
                {
                    using (MemoryStream memStrm = new MemoryStream(bytes))
                    {
                        BinaryFormatter binFtr = new BinaryFormatter();
                        obj = (T)binFtr.Deserialize(memStrm);
                        ok = true;
                    }
                }
            }
            catch
            {
                obj = default(T);
            }

            return ok;
        }

        public static bool SetIt<T>(string key, T obj)
        {
            IDatabase cache;
            byte[] bytTmp = default(byte[]);
            bool ok = false;

            try
            {
                cache = Connection.GetDatabase();

                if (SerializeToBytes(obj, ref bytTmp))
                {
                    cache.StringSet(key, bytTmp);
                    ok = true;
                }
            }
            catch { }

            return ok;
        }

        public static bool SerializeToBytes(object obj, ref byte[] bytes)
        {
            bool ok = false;

            try
            {
                if (obj != null)
                {
                    using (MemoryStream memStrm = new MemoryStream())
                    {
                        BinaryFormatter binFtr = new BinaryFormatter();

                        binFtr.Serialize(memStrm, obj);
                        bytes = memStrm.ToArray();

                        ok = true;
                    }
                }
            }
            catch
            {
                bytes = default(byte[]);
            }

            return ok;
        }
    }
}