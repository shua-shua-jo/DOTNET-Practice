namespace Projector.Models
{
    public class CommandResult
    {
        public CommandResult()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
        public bool IsSuccessful => Errors == null || Errors.Count == 0;

        public static CommandResult Success()
        {
            return new CommandResult();
        }

        public static WithData<T> Success<T>(T data) where T: class, new()
        {
            return new WithData<T>(data);
        }

        public static CommandResult Error(params string[] errors)
        {
            if (errors.Length == 0)
            {
                throw new ArgumentException("errors must have at least one element");
            }

            return new CommandResult
            {
                Errors = errors.ToList()
            };
        }

        public static WithData<T> Error<T>(string v) where T: class, new()
        {
            return new WithData<T>(new T()) { Errors = new List<string> { v } };
        }

        public class WithData<TData> : CommandResult where TData: class, new()
        {
            public WithData(TData data)
            {
                Data = data;
            }

            public TData Data { get; set; }
        }
    }

    
}
