using System;
namespace Platform.Services
{
	public interface ITimeStamper
	{
		string TimeStamp { get; }
	}
    public class DefaultTimeStamper : ITimeStamper
    {
        public string TimeStamp => DateTime.Now.ToShortTimeString();
    }
}

