using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace OutputFileList
{
	[XmlRoot(ElementName = "RegistrationInfo")]
	public class RegistrationInfo
	{
		[XmlElement(ElementName = "URI")]
		public string URI { get; set; }
	}

	[XmlRoot(ElementName = "Principal")]
	public class Principal
	{
		[XmlElement(ElementName = "UserId")]
		public string UserId { get; set; }
		[XmlElement(ElementName = "LogonType")]
		public string LogonType { get; set; }
		[XmlElement(ElementName = "RunLevel")]
		public string RunLevel { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "Principals")]
	public class Principals
	{
		[XmlElement(ElementName = "Principal")]
		public Principal Principal { get; set; }
	}

	[XmlRoot(ElementName = "IdleSettings")]
	public class IdleSettings
	{
		[XmlElement(ElementName = "Duration")]
		public string Duration { get; set; }
		[XmlElement(ElementName = "WaitTimeout")]
		public string WaitTimeout { get; set; }
		[XmlElement(ElementName = "StopOnIdleEnd")]
		public string StopOnIdleEnd { get; set; }
		[XmlElement(ElementName = "RestartOnIdle")]
		public string RestartOnIdle { get; set; }
	}

	[XmlRoot(ElementName = "Settings")]
	public class Settings
	{
		[XmlElement(ElementName = "DisallowStartIfOnBatteries")]
		public string DisallowStartIfOnBatteries { get; set; }
		[XmlElement(ElementName = "StopIfGoingOnBatteries")]
		public string StopIfGoingOnBatteries { get; set; }
		[XmlElement(ElementName = "MultipleInstancesPolicy")]
		public string MultipleInstancesPolicy { get; set; }
		[XmlElement(ElementName = "StartWhenAvailable")]
		public string StartWhenAvailable { get; set; }
		[XmlElement(ElementName = "RunOnlyIfNetworkAvailable")]
		public string RunOnlyIfNetworkAvailable { get; set; }
		[XmlElement(ElementName = "IdleSettings")]
		public IdleSettings IdleSettings { get; set; }
	}

	[XmlRoot(ElementName = "ScheduleByDay")]
	public class ScheduleByDay
	{
		[XmlElement(ElementName = "DaysInterval")]
		public string DaysInterval { get; set; }
	}

	[XmlRoot(ElementName = "CalendarTrigger")]
	public class CalendarTrigger
	{
		[XmlElement(ElementName = "StartBoundary")]
		public string StartBoundary { get; set; }
		[XmlElement(ElementName = "ScheduleByDay")]
		public ScheduleByDay ScheduleByDay { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "Triggers")]
	public class Triggers
	{
		[XmlElement(ElementName = "CalendarTrigger")]
		public CalendarTrigger CalendarTrigger { get; set; }
	}

	[XmlRoot(ElementName = "Exec")]
	public class Exec
	{
		[XmlElement(ElementName = "Command" )]
		public string Command { get; set; }
		[XmlElement(ElementName = "Arguments")]
		public string Arguments { get; set; }
		[XmlElement(ElementName = "WorkingDirectory")]
		public string WorkingDirectory { get; set; }
	}

	[XmlRoot(ElementName = "Actions")]
	public class Actions
	{
		[XmlElement(ElementName = "Exec")]
		public Exec Exec { get; set; }
		[XmlAttribute(AttributeName = "Context")]
		public string Context { get; set; }
	}

	[XmlRoot(ElementName = "Task", Namespace = "http://schemas.microsoft.com/windows/2004/02/mit/task")]
	public class Task
	{
		[XmlElement(ElementName = "RegistrationInfo")]
		public RegistrationInfo RegistrationInfo { get; set; }
		[XmlElement(ElementName = "Principals")]
		public Principals Principals { get; set; }
		[XmlElement(ElementName = "Settings")]
		public Settings Settings { get; set; }
		[XmlElement(ElementName = "Triggers")]
		public Triggers Triggers { get; set; }
		[XmlElement(ElementName = "Actions")]
		public Actions Actions { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
	}

}
