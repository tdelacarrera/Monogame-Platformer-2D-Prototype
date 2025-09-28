
namespace Monogame
{

    public interface IPickable : IGameEntity
    {
        public bool IsPicked { get; set; }
        public void Pickup();
        public void Remove();
    }
}
