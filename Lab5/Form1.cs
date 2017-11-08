using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
	public partial class Form1 : Form
	{
        #region Fields

        CancellationTokenSource m_TokenSource = null;

		#endregion Fields

		#region Constructors
		public Form1()
		{
			InitializeComponent();
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Update the pi output text box
		/// </summary>
		/// <param name="pi">New value for pi</param>
		private void UpdatePiTextBlock(string pi)
		{
            if (piTextBox.InvokeRequired)
            {
                piTextBox.BeginInvoke(new Action<string>(UpdatePiTextBlock), pi);
            }
            else
            {
                piTextBox.Text = pi;
            }
		}

		/// <summary>
		/// Task to calculate an ever-growing number of digits in pi (up to 1,000,000)
		/// </summary>
		private void CalculatePiTask()
		{
            try
            {
                for (int i = 2; i < 1000000; i++)
                {
                    m_TokenSource.Token.ThrowIfCancellationRequested();
                    DateTime start = DateTime.Now;

                    string pi = MathStuff.Calculate(i);
                    UpdatePiTextBlock(pi);

                    DateTime end = DateTime.Now;

                    double totalMs = end.Subtract(start).TotalMilliseconds;
                    if(totalMs < 500)
                    {
                        Thread.Sleep(500 - (int)totalMs);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancelled");
                throw;
            }
		}
		#endregion Methods

		#region Event Handlers
		/// <summary>
		/// Demonstrates Paralel.ForEach
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void parallelGoButton_Click(object sender, EventArgs e)
		{
            var query = from Control child in parallelGroup.Controls
                        where child is ProgressBar
                        select child as ProgressBar;
            Action<ProgressBar, int> updateProgressBarvalue = delegate (ProgressBar pb, int value)
            {
                pb.Value = value;
            };

            Parallel.ForEach(query, progressBar =>
                {
                    for (int i = 0; i < progressBar.Maximum; i++)
                    {
                        if (i % 100 == 0)
                        {
                            progressBar.BeginInvoke(updateProgressBarvalue, progressBar, i);
                        }
                    }
                }

            );
		}

		/// <summary>
		/// Start the pi calculation task
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void taskGoButton_Click(object sender, EventArgs e)
		{
            taskGoButton.Enabled = false;
            taskCancelButton.Enabled = true;

            m_TokenSource = new CancellationTokenSource();
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            Task calculatePiTask = Task.Factory.StartNew(CalculatePiTask, m_TokenSource.Token);

            var resultOK = calculatePiTask.ContinueWith(resultTask =>
            {
                MessageBox.Show("Calculation finished", "Task Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                taskCancelButton.Enabled = false;
                taskGoButton.Enabled = true;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnRanToCompletion,
            ui);

            var resultCancel = calculatePiTask.ContinueWith(resultTask =>
            {
                MessageBox.Show("Calculation stopped by user", "Task Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                taskCancelButton.Enabled = false;
                taskGoButton.Enabled = true;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnCanceled,
            ui);
		}

		private void taskCancelButton_Click(object sender, EventArgs e)
		{
            if (m_TokenSource != null)
            {
                m_TokenSource.Cancel();
            }
		}

		#endregion Event Handlers
	}
}
