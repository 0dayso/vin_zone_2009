using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RouteInformation
    {
        #region �ֶ�
        private DateTime airDate;
        private string originalAirport;
        private string destinationAirport;
        private string airLine;
        private string flightNO;
        private DateTime departureTime;
        private DateTime arriveTime;
        private string flightType;
        private string meal;
        private double fuelTax;
        private double airportTax;
        private double yprice;
        private string changeRule;
        private string flightInterval;
        private double discount;
        private double ticketPrice;
        private string cabin;
        private int stops;

        IList<Seat> seatList;

        /// <summary>
        /// ��ȡ����rule�����е�������
        /// </summary>
        private DateTime eyearmonthday;

        //�������ۿ�
        private string ediscount;

      
        /// <summary>
        /// ˰��
        /// </summary>
        private double eairportfuel;
        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime AirDate
        {
            get
            {
                return airDate;
            }
            set
            {
                if (this.airDate != value)
                    this.airDate = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string OriginalAirport
        {
            get
            {
                return originalAirport;
            }
            set
            {
                if (this.originalAirport != value)
                    this.originalAirport = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string DestinationAirport
        {
            get
            {
                return destinationAirport;
            }
            set
            {
                if (this.destinationAirport != value)
                    this.destinationAirport = value;
            }
        }

        /// <summary>
        /// ���չ�˾
        /// </summary>
        public string AirLine
        {
            get
            {
                return airLine;
            }
            set
            {
                if (this.airLine != value)
                    this.airLine = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string FlightNO
        {
            get
            {
                return flightNO;
            }
            set
            {
                if (this.flightNO != value)
                    this.flightNO = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime DepartureTime
        {
            get
            {
                return departureTime;
            }
            set
            {
                if (this.departureTime != value)
                    this.departureTime = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ArriveTime
        {
            get
            {
                return arriveTime;
            }
            set
            {
                if (this.arriveTime != value)
                    this.arriveTime = value;
            }
        }

        /// <summary>
        /// �����ͺ�
        /// </summary>
        public string FlightType
        {
            get
            {
                return flightType;
            }
            set
            {
                if (this.flightType != value)
                    this.flightType = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public string Meal
        {
            get
            {
                return meal;
            }
            set
            {
                if (this.meal != value)
                {
                    this.meal = value;
                }
            }
        }

        /// <summary>
        /// ȼ��
        /// </summary>
        public double FuelTax
        {
            get
            {
                return fuelTax;
            }
            set
            {
                if (this.fuelTax != value)
                    this.fuelTax = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public double AirportTax
        {
            get
            {
                return airportTax;
            }
            set
            {
                if (this.airportTax != value)
                    this.airportTax = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public double Yprice
        {
            get
            {
                return yprice;
            }
            set
            {
                if (this.yprice != value)
                    this.yprice = value;
            }
        }

        /// <summary>
        /// �˸�ǩ
        /// </summary>
        public string ChangeRule
        {
            get
            {
                return changeRule;
            }
            set
            {
                if (this.changeRule != value)
                    this.changeRule = value;
            }
        }


        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string FlightInterval
        {
            get
            {
                return flightInterval;
            }
            set
            {
                if (this.flightInterval != value)
                    this.flightInterval = value;
            }
        }

        /// <summary>
        /// �ۿ�
        /// </summary>
        public double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                if (this.discount != value)
                    this.discount = value;
            }
        }

        /// <summary>
        /// �۸�
        /// </summary>
        public double TicketPrice
        {
            get
            {
                return ticketPrice;
            }
            set
            {
                if (this.ticketPrice != value)
                    this.ticketPrice = value;
            }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string Cabin
        {
            get
            {
                return cabin;
            }
            set
            {
                if (this.cabin != value)
                    this.cabin = value;
            }
        }

        /// <summary>
        /// ��ͣ
        /// </summary>
        public int Stops
        {
            get { return stops; }
            set
            {
                if (this.stops != value)
                    this.stops = value;
            }
        }


        /// <summary>
        /// ��ȡ��λ�۸��б�
        /// </summary>
        public IList<Seat> SeatList
        {
            get
            {
                return seatList;
            }
            set
            {
                if (this.seatList != value)
                    this.seatList = value;
            }
        }

        /// <summary>
        /// �������ۿ�
        /// </summary>
        public string Ediscount
        {
            get { return ediscount; }
            set
            {
                if (this.ediscount != value)
                    this.ediscount = value;
            }
        }

        /// <summary>
        /// ˰��
        /// </summary>
        public double Eairportfuel
        {
            get { return eairportfuel; }
            set
            {
                if (this.eairportfuel != value)
                    this.eairportfuel = value;
            }
        }

        /// <summary>
        /// ��ȡ����rule�����е�������
        /// </summary>
        public DateTime Eyearmonthday
        {
            get { return eyearmonthday; }
            set
            {
                if (this.eyearmonthday != value)
                    this.eyearmonthday = value;
            }
        }
        #endregion
    }
}
