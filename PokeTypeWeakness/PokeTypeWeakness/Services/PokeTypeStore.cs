using Newtonsoft.Json;
using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeTypeWeakness.Services
{
    public class PokeTypeStore : IDataStore<PokeType>
    {
        private IEnumerable<PokeType> pokeTypes = null;

        public async Task<PokeType> GetItemAsync(string id)
        {
            if (pokeTypes == null)
                await GetItemsAsync(true);

            return pokeTypes.SingleOrDefault(x => x.NaturalID == id);
        }

        public async Task<IEnumerable<PokeType>> GetItemsAsync(bool forceRefresh = false)
        {
            if(pokeTypes == null || forceRefresh)
                pokeTypes = await LoadFromJson();

            return pokeTypes;
        }

        private async Task<IEnumerable<PokeType>> LoadFromJson()
        {
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly;
            Stream stream = assembly.GetManifestResourceStream("PokeTypeWeakness.Assets.PokeTypes.json");

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
                IEnumerable<PokeType> loadedPokeTypes = JsonConvert.DeserializeObject<IEnumerable<PokeType>>(json);
                return loadedPokeTypes;
            }
        }
    }
}
