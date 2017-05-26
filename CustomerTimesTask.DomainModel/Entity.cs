namespace CustomerTimesTask.DomainModel
{
    public abstract class Entity<TKey> : Entity
    {
        #region properties

        public virtual TKey Id { get; protected set; }

        #endregion properties

        #region constructors

        protected Entity(TKey id)
        {
            Id = id;
        }

        #endregion constructors
    }

    public abstract class Entity
    {
    }
}