namespace KhanateJob.DAL.IRepositories;

public interface IGenericRepository<TResult>
{
    Task<TResult> CreatAsync(TResult value);
    Task<TResult> UpdateAsync(long Id, TResult value);
    Task<bool> DeleteAsnyc(long Id);
    Task<List<TResult>> GetAllAsync();
    Task<TResult> GetByIdAsync(long id);
}
