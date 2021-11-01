using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLiteNetExtensionsAsync.Extensions;

namespace sourceTree
{
    public class DatabaseManager
    {

        SQLiteAsyncConnection _connection;

    public DatabaseManager()
    {
        _connection = DependencyService.Get<SQLiteDBInterface>().createSQLiteDB();

    }

    public async Task<ObservableCollection<Topic>> CreateTabel()
    {
            // create new table if it does not exist
        await _connection.CreateTableAsync<Topic>();
        

            // select * from ToDoTask
            //
            var apple = from s in _connection.Table<Topic>()
                    where s.isSub == false
                    select s;

        var query = _connection.Table<Topic>().Where(i => i.isSub == false).OrderBy(i => i.Id);
        var count = await query.CountAsync();
        if (count == 0) {
            return new ObservableCollection<Topic>();
        }
        var listTable = await query.ToListAsync();
        var RootTopics = new ObservableCollection<Topic>(listTable);
        
        return RootTopics;
    }

        public async Task<Topic> getTopic(int id)
        {
            return await _connection.GetWithChildrenAsync<Topic>(id);
        }
      
        public async void InsertSubTopic(Topic newTopic, Topic parentTopic)
           {

            await _connection.InsertAsync(newTopic);
            if (parentTopic.subTopics.Count == 0)
            {
                parentTopic.subTopics = new List<Topic> { newTopic };
                
            } else
            {
                parentTopic.subTopics.Add(newTopic);
            }
            //await _connection.UpdateAsync(parentTopic);
            await _connection.UpdateWithChildrenAsync(parentTopic);

        }
        public async void InsertTopic(Topic newTopic)
        {
            await _connection.InsertAsync(newTopic);
        }

        public async void updateTopic(Topic objUpdate)
        {
            await _connection.UpdateAsync(objUpdate);
        }

        //public async void deleteToDo(toDoTask toDelete)
        //{
        //    // update where id == id 
        //    await _connection.DeleteAsync(toDelete);
        //}

    }
}
