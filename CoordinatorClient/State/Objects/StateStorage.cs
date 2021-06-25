namespace CoordinatorClient.State.Objects
{
    public class StateStorage<T>
    {
        public static readonly StateStorage<T> Instance = new StateStorage<T>();

        public T State { get; set; }

        private StateStorage()
        {

        }
    }
}
