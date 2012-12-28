using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightLowestPrice
    {
        #region �ֶ�
        /// <summary>
        /// ��ͼ۸�id
        /// </summary>
        private Int32 _LowestPriceId;

        /// <summary>
        /// ������
        /// </summary>
        private String _Departure;

        /// <summary>
        /// Ŀ�ĵ�
        /// </summary>
        private String _Arrival;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime? _CreateDate;

        /// <summary>
        /// ��ͼ۸�
        /// </summary>
        private Double? _LowestPrice;

        /// <summary>
        /// ���ʱ��
        /// </summary>
        private DateTime? _DepartureTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime? _ArrivalTime;

        /// <summary>
        /// ���չ�˾
        /// </summary>
        private String _Airliner;

        /// <summary>
        /// �����
        /// </summary>
        private String _Flight;

        /// <summary>
        /// ��λ
        /// </summary>
        private String _Cabin;

        #endregion

        #region ����
        /// <summary>
        /// ��ͼ۸�id
        /// </summary>
        public Int32 LowestPriceId
        {
            get { return _LowestPriceId; }
            set { this._LowestPriceId = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public String Departure
        {
            get { return _Departure; }
            set { this._Departure = value; }
        }

        /// <summary>
        /// Ŀ�ĵ�
        /// </summary>
        public String Arrival
        {
            get { return _Arrival; }
            set { this._Arrival = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { this._CreateDate = value; }
        }

        /// <summary>
        /// ��ͼ۸�
        /// </summary>
        public Double? LowestPrice
        {
            get { return _LowestPrice; }
            set { this._LowestPrice = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime? DepartureTime
        {
            get { return _DepartureTime; }
            set { this._DepartureTime = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? ArrivalTime
        {
            get { return _ArrivalTime; }
            set { this._ArrivalTime = value; }
        }

        /// <summary>
        /// ���չ�˾
        /// </summary>
        public String Airliner
        {
            get { return _Airliner; }
            set { this._Airliner = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public String Flight
        {
            get { return _Flight; }
            set { this._Flight = value; }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public String Cabin
        {
            get { return _Cabin; }
            set { this._Cabin = value; }
        }

        #endregion
    }
}
