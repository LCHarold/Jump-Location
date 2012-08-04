using System;

namespace Jump.Location
{
    public interface IRecord
    {
        string Provider { get; }
        string Path { get; }
        string FullName { get; set; }
        decimal Weight { get; }
        void AddTimeSpent(TimeSpan timeSpan);
    }

    class Record : IRecord
    {
        public Record(string fullName, decimal weight)
        {
            FullName = fullName;
            Weight = weight;
        }

        public Record(string fullName)
            :this(fullName, 0)
        {
        }

        public string Provider { get; private set; }
        public string Path { get; private set; }

        public string FullName
        {
            get { return string.Format("{0}::{1}", Provider, Path); }
            set
            {
                var parts = value.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

                if (parts == null || parts.Length != 2)
                    throw new ArgumentException("Expected FullName in format of 'Provider::Path' but got " + value);

                Provider = parts[0];
                Path = parts[1];
            }
        }

        public decimal Weight { get; private set; }

        public void AddTimeSpent(TimeSpan timeSpan)
        {
            Weight += (decimal) timeSpan.TotalSeconds;
        }
    }
}