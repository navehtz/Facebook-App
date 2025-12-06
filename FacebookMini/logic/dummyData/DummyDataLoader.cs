using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWinFormsApp.utils;
using Newtonsoft.Json;

namespace FacebookWinFormsApp.logic.dummyData
{
    public static class DummyDataLoader
    {
        public static List<DummyUser> LoadDummyFriends()
        {
            string path = Constants.sr_DummyFriendsJsonPath;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("friends.json not found!", path);
            }

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<DummyUser>>(json);
        }

        public static List<DummyPost> LoadDummyPosts()
        {
            string path = Constants.sr_DummyPostsJsonPath;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("posts.json not found!", path);
            }
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<DummyPost>>(json);
        }

        public static List<DummyPhoto> LoadDummyPhotos()
        {
            string path = Constants.sr_DummyPhotosJsonPath;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("photos.json not found!", path);
            }
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<DummyPhoto>>(json);
        }
    }
}
