namespace _Scripts.Interfaces
{
    public interface IEvent
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        //public SceneObjects Objects { get; set; }
        public void ApplyEvent();
    }
}
