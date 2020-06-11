using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.API.Models
{
	public class ResponseModel
	{
		public string Status => "Ok";
		public object Data { get; set; }

		public ResponseModel(object data)
		{
			Data = data;
		}
	}
}
