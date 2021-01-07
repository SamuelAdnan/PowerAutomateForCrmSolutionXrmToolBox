using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerAutomateForCrmSolution.Models
{
	class AllModels
	{
	}
	public class FlowValue
	{

		public int statecode { get; set; }
		public string workflowid { get; set; }
		public string name { get; set; }
	}

	public class RootFlow
	{

		public List<FlowValue> value { get; set; }
	}

	internal class Blink
	{
		int BlinkCount = 0;
		private Label _info;
		private Timer _tmrBlink;

		public void Text(Label info, string message)
		{
			_info = info;
			_info.Text = message;
			_tmrBlink = new Timer();
			_tmrBlink.Interval = 300;
			_tmrBlink.Tick += new System.EventHandler(tmrBlink_Tick);
			_tmrBlink.Start();
		}

		private void tmrBlink_Tick(object sender, EventArgs e)
		{
			BlinkCount++;

			if (_info.BackColor == System.Drawing.Color.Khaki)
			{
				_info.BackColor = System.Drawing.Color.Transparent;
			}
			else
			{
				_info.BackColor = System.Drawing.Color.Khaki;
			}

			if (BlinkCount == 9)
			{
				_tmrBlink.Stop();
				_info.Text = "";
				BlinkCount = 0;
			}
		}
	}


	public class PublisherValue
	{

		public string friendlyname { get; set; }
		public string publisherid { get; set; }
	}

	public class Publisher
	{
		public List<PublisherValue> value { get; set; }
	}

	public class SoltuionValue
	{

		public string friendlyname { get; set; }
		public string uniquename { get; set; }
		public string solutionid { get; set; }
	}

	public class Soltuion
	{
		public List<SoltuionValue> value { get; set; }
	}




	public class CustomComboboxItem
	{
		public string Text { get; set; }
		public object Value { get; set; }
		public string extra { get; set; }
		public override string ToString()
		{
			return Text;
		}
	}
}
