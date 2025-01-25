using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormMatrixPattern : Form
    {
        private int m_Width = 60; // Cell width
        private int m_Height = 60; // Cell height
        private int m_NoOfRows = 2; // Initial number of rows
        private int m_NoOfColumns = 2; // Initial number of columns
        private int count = 8; // Maximum grid size (8x8)
        private CancellationTokenSource _cts;

        private Thread counterThread; // Counter thread to control animation

        public FormMatrixPattern()
        {
            InitializeComponent();
        }

        private void Start(object sender, EventArgs e)
        {
            // Cancel any existing animation
            _cts = new CancellationTokenSource();
            // Start the counter thread
            counterThread = new Thread(new ThreadStart(AnimateGrid));
            counterThread.Start();
        }

        private void Pause(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void Stopandclear(object sender, EventArgs e)
        {
            _cts?.Cancel();
            m_NoOfRows = 2;  // Reset the grid to default size
            m_NoOfColumns = 2;
            Invalidate(); // Clears the screen
        }

        // Thread-controlled grid animation
        private void AnimateGrid()
        {
            while (true)
            {
                for (int i = 2; i <= count; i++)
                {
                    if (_cts.Token.IsCancellationRequested) break;

                    m_NoOfRows = i;
                    m_NoOfColumns = i;

                    // Update grid size and redraw
                    Invoke(new Action(() => Invalidate())); // Request UI update

                    Thread.Sleep(500); // Delay to animate

                }

                // After reaching maximum size, reverse animation to decrease the grid size
                for (int i = count - 1; i >= 2; i--)
                {
                    if (_cts.Token.IsCancellationRequested) break;

                    m_NoOfRows = i;
                    m_NoOfColumns = i;

                    // Update grid size and redraw
                    Invoke(new Action(() => Invalidate())); // Request UI update

                    Thread.Sleep(500); // Delay to animate
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics boardLayout = e.Graphics;
            Pen layoutPen = new Pen(Color.Red) { Width = 5 };

            int x = 100, y = 100;

            // Draw rows based on current grid size
            for (int r = 0; r <= m_NoOfRows; r++)
            {
                boardLayout.DrawLine(layoutPen, x, y, x + m_NoOfColumns * m_Width, y);
                y += m_Height;
            }

            // Draw columns based on current grid size
            x = 100;
            y = 100;
            for (int c = 0; c <= m_NoOfColumns; c++)
            {
                boardLayout.DrawLine(layoutPen, x, y, x, y + m_NoOfRows * m_Height);
                x += m_Width;
            }
        }

        // UI Controls: Update grid size based on user input
        /*
        private void toolStripMenuItem2_Click(object sender, EventArgs e) => count = 3;
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => count = 4;
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => count = 5;
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => count = 6;
        private void toolStripMenuItem6_Click(object sender, EventArgs e) => count = 7;
        private void ToolStripMenuItem7_Click(object sender, EventArgs e) => count = 8;

        private void UpdateGridSize(int gridSize)
        {
            count = gridSize; // Update the grid size
            m_NoOfRows = gridSize;
            m_NoOfColumns = gridSize;
            Invalidate(); // Redraw the form with the new grid
        } 


        private void toolStripMenuItem8_Click(object sender, EventArgs e) => count = 3;
        private void toolStripMenuItem9_Click(object sender, EventArgs e) => count = 4;
        private void toolStripMenuItem10_Click(object sender, EventArgs e) => count = 5;
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => count = 6;
        private void toolStripMenuItem12_Click(object sender, EventArgs e) => count = 7;
        private void toolStripMenuItem13_Click(object sender, EventArgs e) => count = 8;
        */

        // Add a common method to update grid size (NEW)
        private void UpdateGridSize(int gridSize)
        {
            count = gridSize; // Update the grid size
            m_NoOfRows = 2;
            m_NoOfColumns = 2;
            Invalidate(); // Redraw the form with the new grid
        }

        // Menu event handlers (UPDATED)
        private void toolStripMenuItem2_Click(object sender, EventArgs e) => UpdateGridSize(3);
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => UpdateGridSize(4);
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => UpdateGridSize(5);
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => UpdateGridSize(6);
        private void toolStripMenuItem6_Click(object sender, EventArgs e) => UpdateGridSize(7);
        private void ToolStripMenuItem7_Click(object sender, EventArgs e) => UpdateGridSize(8);

        // Toolbar event handlers (UPDATED)
        private void toolStripMenuItem8_Click(object sender, EventArgs e) => UpdateGridSize(3);
        private void toolStripMenuItem9_Click(object sender, EventArgs e) => UpdateGridSize(4);
        private void toolStripMenuItem10_Click(object sender, EventArgs e) => UpdateGridSize(5);
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => UpdateGridSize(6);
        private void toolStripMenuItem12_Click(object sender, EventArgs e) => UpdateGridSize(7);
        private void toolStripMenuItem13_Click(object sender, EventArgs e) => UpdateGridSize(8);
    }
}





