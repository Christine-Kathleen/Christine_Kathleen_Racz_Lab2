using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;

namespace Christine_Kathleen_Racz_Lab2
{
    class DoughnutMachine : Component
    {
        private System.Collections.ArrayList mDoughnuts = new System.Collections.ArrayList(); // memoreaza instanțe din clasa Doughnut.
        public Doughnut this[int Index]
        {
            get
            {
     
                return (Doughnut)mDoughnuts[Index];

            }
            set
            {
                mDoughnuts[Index] = value;
            }
        }
        private DoughnutType mFlavor;
        public DoughnutType Flavor // adăugăm o proprietate care să indice tipul de gogoașă generat de
                                    //instanta curentă din clasă.Proprietatea trebuie să fie de tipul enumerare DoughnutType
        {
            get
            {
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }
        public delegate void DoughnutCompleteDelegate();//eveniment care se va genera atunci cand crearea unei gogoși este finalizată. Pentru acest
                                                        //eveniment trebuie să creem si un Delegate public in clasa DoughnutMachine
        public event DoughnutCompleteDelegate DoughnutComplete;

        DispatcherTimer doughnutTimer;//monitoriza timpul de coacere pentru fiecare gogoașă

        private void InitializeComponent() //pt a initializa obiectul doughnutTimer
        {
            this.doughnutTimer = new DispatcherTimer();
            this.doughnutTimer.Tick += new System.EventHandler(this.doughnutTimer_Tick);
        }
        public DoughnutMachine() //ne asigurăm că această metodă este apelată la creerea unei instanțe din component DoughnutMachine
        {
            InitializeComponent();
        }
        private void doughnutTimer_Tick(object sender, EventArgs e)
        {
            Doughnut aDoughnut = new Doughnut(this.Flavor);
            mDoughnuts.Add(aDoughnut);
            DoughnutComplete();
        }

        public bool Enabled
        {
            set
            {
                doughnutTimer.IsEnabled = value;
            }
        }
        public int Interval
        {
            set
            {
                doughnutTimer.Interval = new TimeSpan(0, 0, value);
            }
        }
        public void MakeDoughnuts(DoughnutType dFlavor)
        {

            Flavor = dFlavor;
            switch (Flavor)
            {
                case DoughnutType.Glazed: Interval = 3; break;
                case DoughnutType.Sugar: Interval = 2; break;
                case DoughnutType.Lemon: Interval = 5; break;
                case DoughnutType.Chocolate: Interval = 7; break;
                case DoughnutType.Vanilla: Interval = 4; break;
            }
            doughnutTimer.Start();
        }
    }
    public enum DoughnutType //e adăugăm un tip enumerare 
    {
        Glazed,
        Sugar,
        Lemon,
        Chocolate,
        Vanilla
    }
    class Doughnut // clasă numită Doughnut care reprezintă o
                    //gogoașă.Această clasă trebuie să aibă o proprietate numită flavor, una price si o proprietate time(de tip
                    //read-only)
    {
        private DoughnutType mFlavor;

        public DoughnutType Flavor
        {
            get
            {
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }
        private float mPrice = .50F;
        public float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }
        private readonly DateTime mTimeOfCreation;
        public DateTime TimeOfCreation
        {
            get
            {
                return mTimeOfCreation;
            }

        }
        // proprietătile de tip readonly pot să fie initializate doar in constructor, pentru această clasă va trebui să scriem un
        //constructor care să seteze proprietățile obiectului de tip Doughnut creat
        public Doughnut(DoughnutType aFlavor) // constructor
        {
            mTimeOfCreation = DateTime.Now;
            mFlavor = aFlavor;
        }
    }
}
