namespace Keeper.API.Models
{
    public class ResponseModel<T>
	{
		public string Status => "Ok";
		public T Data { get; set; }

		public ResponseModel(T data)
		{
			Data = data;
		}
	}
}
