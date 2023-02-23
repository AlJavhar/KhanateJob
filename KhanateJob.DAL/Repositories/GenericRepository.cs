using KhanateJob.DAL.Configurations;
using KhanateJob.DAL.IRepositories;
using KhanateJob.Domain.Commons;
using KhanateJob.Domain.Entities;
using Newtonsoft.Json;


namespace KhanateJob.DAL.Repositories;

public class GenericRepository<TResult> : IGenericRepository<TResult> where TResult : Auditable
{
    private string path;
    private long lastId;

    public GenericRepository()
    {
        if (typeof(TResult) == typeof(Users))
        {
            path = DataBasesPath.USERS_DB;
        }
        else if (typeof(TResult) == typeof(Sellers))
        {
            path = DataBasesPath.SELLERS_DB;
        }
        else if (typeof(TResult) == typeof(Resumes))
        {
            path = DataBasesPath.RESUMES_DB;
        }
        else if (typeof(TResult) == typeof(JobTables))
        {
            path = DataBasesPath.JOBS_DB;
        }
        else if (typeof(TResult) == typeof(CheckedJobs))
        {
            path = DataBasesPath.CHECKEDJOBS_DB;
        }
        else if (typeof(TResult) == typeof(LikeJobs))
        {
            path = DataBasesPath.LIKEDJOBS_DB;
        }
    }
    public async Task<TResult> CreatAsync(TResult value)
    {
        

        value.CreatedAt = DateTime.UtcNow;
        var values = await GetAllAsync();

        if(values.Count != 0)
       lastId = values.Max(x => x.Id);
        value.Id = ++lastId;

        values.Add(value);

        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);
        return value;
    }

    public async Task<bool> DeleteAsnyc(long id)
    {
        var values = await GetAllAsync();
        var value = values.FirstOrDefault(x => x.Id == id);

        if (value is null)
            return false;

        values.Remove(value);
        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);

        return true;
    }

    public async Task<List<TResult>> GetAllAsync()
    {
        string models = await File.ReadAllTextAsync(path);
        if (string.IsNullOrEmpty(models))
            models = "[]";

        List<TResult> results = JsonConvert.DeserializeObject<List<TResult>>(models);
        return results;
    }

    public async Task<TResult> GetByIdAsync(long id)
    {
        var values = await GetAllAsync();
        return values.FirstOrDefault(x => x.Id == id);
    }

    public async Task<TResult> UpdateAsync(long id, TResult value)
    {
        var values = await GetAllAsync();
        var model = values.FirstOrDefault(x => x.Id == id);
        if (model is not null)
        {
            var index = values.IndexOf(model);
            values.Remove(model);

            value.CreatedAt = model.CreatedAt;
            value.UpdatedAt = DateTime.UtcNow;

            values.Insert(index, value);
            var json = JsonConvert.SerializeObject(values, Formatting.Indented);
            await File.WriteAllTextAsync(path, json);
            return model;
        }

        return model;
    }
}
