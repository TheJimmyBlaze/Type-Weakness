using Newtonsoft.Json;
using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokeTypeWeakness.Services
{
    public class AttributionStore : IDataStore<Attribution>
    {
        private IEnumerable<Attribution> attributions = null;

        public Task<Attribution> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attribution>> GetItemsAsync(bool forceRefresh = false)
        {
            if (attributions == null || forceRefresh)
                attributions = await LoadFromJson();

            return attributions;
        }

        private async Task<IEnumerable<Attribution>> LoadFromJson()
        {
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly;
            Stream stream = assembly.GetManifestResourceStream("PokeTypeWeakness.Assets.Attributions.json");

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
                IEnumerable<Attribution> loadedAttributions = JsonConvert.DeserializeObject<IEnumerable<Attribution>>(json);
                return loadedAttributions;
            }
        }
    }
}
