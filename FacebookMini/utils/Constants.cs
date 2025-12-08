using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookWinFormsApp.utils
{
    public static class Constants
    {
        public const string k_AppId = "806995989056767";

        private static readonly string sr_BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string sr_JsonsDir =
            Path.Combine(sr_BaseDir, "Resources", "jsons");

        public static readonly string sr_DummyFriendsJsonPath =
            Path.Combine(sr_JsonsDir, "friends.json");

        public static readonly string sr_DummyPostsJsonPath =
            Path.Combine(sr_JsonsDir, "posts.json");

        public static readonly string sr_DummyPhotosJsonPath =
            Path.Combine(sr_JsonsDir, "photos.json");
    }
}
