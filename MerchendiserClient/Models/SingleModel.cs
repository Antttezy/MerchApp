namespace MerchendiserClient.Models
{
    public class SingleModel<T> : ObservableObject
    {
        T value;

        public T Value
        {
            get => value;

            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public SingleModel(T Value)
        {
            this.Value = Value;
        }

        public SingleModel()
        {

        }
    }
}
