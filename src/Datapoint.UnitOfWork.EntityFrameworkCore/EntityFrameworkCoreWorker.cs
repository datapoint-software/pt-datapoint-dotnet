namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <inheritdoc />
    public abstract class EntityFrameworkCoreWorker<TEntityFrameworkCoreUnitOfWork> : IWorker
        where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork
    {
        /// <summary>
        /// Creates a new worker.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected EntityFrameworkCoreWorker(TEntityFrameworkCoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        protected TEntityFrameworkCoreUnitOfWork UnitOfWork { get; }
    }
}
