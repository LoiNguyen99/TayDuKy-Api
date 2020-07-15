using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Services
{
    public class FileServices
    {
        public static async Task<String> getFile(Stream fileStream, String fileName, String apiKey)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var login = await auth.SignInAnonymouslyAsync();
            var task = new Firebase.Storage.FirebaseStorage("tayduky-d785d.appspot.com", new Firebase.Storage.FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(login.FirebaseToken)
            }).Child("equipments").Child(fileName).PutAsync(fileStream);

            return await task;

        }

        public static async Task<String> getDoc(Stream fileStream, String fileName, String apiKey)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var login = await auth.SignInAnonymouslyAsync();
            var task = new Firebase.Storage.FirebaseStorage("tayduky-d785d.appspot.com", new Firebase.Storage.FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(login.FirebaseToken)
            }).Child("docs").Child(fileName).PutAsync(fileStream);

            return await task;

        }
    }
}
