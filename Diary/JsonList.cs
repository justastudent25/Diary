using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    internal class JsonList
    {
        public void IntoJson(Dictionary<string, Note> notes_dict)
        {
            string json = JsonConvert.SerializeObject(notes_dict);
            File.WriteAllText("C:\\Users\\кальмар в эболе\\source\\repos\\Diary2\\Diary_2-main\\Diary\\Diary\\notes.json", json);
        }

        public Dictionary<string, Note> Outtojson()
        {
            Dictionary<string, Note> notes = JsonConvert.DeserializeObject<Dictionary<string, Note>>(File.ReadAllText("C:\\Users\\кальмар в эболе\\source\\repos\\Diary2\\Diary_2-main\\Diary\\Diary\\notes.json"));
            return notes;
        }
    }
}
