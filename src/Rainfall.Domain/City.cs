namespace Rainfall.Domain
{
    public class City:IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}