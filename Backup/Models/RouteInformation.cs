using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RouteInformation
    {
        #region 字段
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
        /// 获取艺龙rule数据中的年月日
        /// </summary>
        private DateTime eyearmonthday;

        //艺龙的折扣
        private string ediscount;

      
        /// <summary>
        /// 税费
        /// </summary>
        private double eairportfuel;
        #endregion

        #region 属性

        /// <summary>
        /// 航班日期
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
        /// 出发地
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
        /// 到达地
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
        /// 航空公司
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
        /// 航班号
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
        /// 出发时间
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
        /// 到达时间
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
        /// 航班型号
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
        /// 餐饮
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
        /// 燃油
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
        /// 机建
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
        /// 公开价
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
        /// 退改签
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
        /// 飞行时长
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
        /// 折扣
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
        /// 价格
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
        /// 舱位
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
        /// 经停
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
        /// 获取舱位价格列表
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
        /// 艺龙的折扣
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
        /// 税费
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
        /// 获取艺龙rule数据中的年月日
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
