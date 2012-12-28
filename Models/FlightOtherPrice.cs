using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightOtherPrice
    {
       

        #region �ֶ�
        /// <summary>
        /// 
        /// </summary>
        private Int32 _Otherpriceid;

        /// <summary>
        /// �۸���Դ
        /// </summary>
        private Int32? _Sourcetype;

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
        /// 
        /// </summary>
        public Int32 Otherpriceid
        {
            get { return _Otherpriceid; }
            set { this._Otherpriceid = value; }
        }

        /// <summary>
        /// �۸���Դ
        /// </summary>
        public Int32? Sourcetype
        {
            get { return _Sourcetype; }
            set { this._Sourcetype = value; }
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
