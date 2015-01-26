using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class Document: AbonentCover
    {
        private Int64? documentcd;
        /// <summary>
        /// DOCUMENTCD         IDENTIFER NOT NULL /* IDENTIFER = INTEGER */,
        /// </summary>
        public Int64? Documentcd
        {
            get { return documentcd; }
            set { documentcd = value; }
        }

        private int? organizationcd;
        /// <summary>
        /// ORGANIZATIONCD     PKFIELD /* PKFIELD = INTEGER */,
        /// </summary>
        public int? Organizationcd
        {
            get { return organizationcd; }
            set { organizationcd = value; }
        }

        private int? registerusercd;
        /// <summary>
        /// REGISTERUSERCD     PKFIELD /* PKFIELD = INTEGER */,
        /// </summary>
        public int? Registerusercd
        {
            get { return registerusercd; }
            set { registerusercd = value; }
        }

        private int? otvetstvusercd;
        /// <summary>
        /// OTVETSTVUSERCD     PKFIELD /* PKFIELD = INTEGER */,
        /// </summary>
        public int? Otvetstvusercd
        {
            get { return otvetstvusercd; }
            set { otvetstvusercd = value; }
        }

        private int? doctypeid;
        /// <summary>
        /// DOCTYPEID          KEYS /* KEYS = INTEGER */,
        /// </summary>
        public int? Doctypeid
        {
            get { return doctypeid; }
            set { doctypeid = value; }
        }

        private string docname;
        /// <summary>
        /// DOCNAME            VARCHAR(150),
        /// </summary>
        public string Docname
        {
            get { return docname; }
            set { docname = value; }
        }

        private string doc_number;
        /// <summary>
        /// DOC_NUMBER         VARCHAR(50),
        /// </summary>
        public string Doc_number
        {
            get { return doc_number; }
            set { doc_number = value; }
        }

        private string doc_ser;
        /// <summary>
        /// DOC_SER            VARCHAR(20),
        /// </summary>
        public string Doc_ser
        {
            get { return doc_ser; }
            set { doc_ser = value; }
        }

        private DateTime? docdate;
        /// <summary>
        /// DOCDATE            TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Docdate
        {
            get { return docdate; }
            set { docdate = value; }
        }

        private DateTime? inputdate;
        /// <summary>
        /// INPUTDATE          TIMESTAMP,
        /// </summary>
        public DateTime? Inputdate
        {
            get { return inputdate; }
            set { inputdate = value; }
        }

        private DateTime? outputdate;
        /// <summary>
        /// OUTPUTDATE         TIMESTAMP,
        /// </summary>
        public DateTime? Outputdate
        {
            get { return outputdate; }
            set { outputdate = value; }
        }

        private DateTime? factdocumentdate;
        /// <summary>
        /// FACTDOCUMENTDATE   TIMESTAMP,
        /// </summary>
        public DateTime? Factdocumentdate
        {
            get { return factdocumentdate; }
            set { factdocumentdate = value; }
        }

        private string documentidentifer;
        /// <summary>
        /// DOCUMENTIDENTIFER  VARCHAR(45),
        /// </summary>
        public string Documentidentifer
        {
            get { return documentidentifer; }
            set { documentidentifer = value; }
        }

        private int? internaluseonly;
        /// <summary>
        /// INTERNALUSEONLY    INTEGER,
        /// </summary>
        public int? Internaluseonly
        {
            get { return internaluseonly; }
            set { internaluseonly = value; }
        }

        private int? employee;
        /// <summary>
        /// EMPLOYEE           INTEGER,
        /// </summary>
        public int? Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        private bool? fromsuperior;
        /// <summary>
        /// FROMSUPERIOR       BOOLEAN /* BOOLEAN = SMALLINT CHECK (VALUE IN (0, 1)) */,
        /// </summary>
        public bool? Fromsuperior
        {
            get { return fromsuperior; }
            set { fromsuperior = value; }
        }

        private int? status;
        /// <summary>
        /// STATUS             INTEGER,
        /// </summary>
        public int? Status
        {
            get { return status; }
            set { status = value; }
        }

        private int? reasonid;
        /// <summary>
        /// REASONID           PKFIELD /* PKFIELD = INTEGER */
        /// </summary>
        public int? Reasonid
        {
            get { return reasonid; }
            set { reasonid = value; }
        }

        public string InsertRecord
        {
            get
            {
                // INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, 
                // DOCTYPEID, DOCNAME, DOC_NUMBER, DOC_SER, DOCDATE, INPUTDATE, OUTPUTDATE, FACTDOCUMENTDATE, 
                // DOCUMENTIDENTIFER, INTERNALUSEONLY, EMPLOYEE, FROMSUPERIOR, STATUS, REASONID) 
                //  VALUES (1018491, NULL, 1, 1, 12, '->Оп.-''SYSDBA'';дата:''27.01.2012'',станция:''abn''', 
                //          '0', '0', '27-JAN-2012', '27-JAN-2012 00:00:00', '27-JAN-2012 00:00:00', 
                //          '27-JAN-2012 09:36:30', '001D1090162062595305864', NULL, NULL, NULL, NULL, NULL);                
                string s = String.Format(
                     "INSERT INTO DOCUMENTS (DOCUMENTCD, ORGANIZATIONCD, REGISTERUSERCD, OTVETSTVUSERCD, " +
                        "DOCTYPEID, DOCNAME, DOC_NUMBER, DOC_SER, DOCDATE, INPUTDATE, OUTPUTDATE, FACTDOCUMENTDATE, " +
                        "DOCUMENTIDENTIFER, INTERNALUSEONLY, EMPLOYEE, FROMSUPERIOR, STATUS, REASONID) " +
                        "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, " +
                        "{6}, {7}, {8}, {9}, {10}, " +
                        "{11}, {12}, {13}, {14}, {15}, {16}, {17});",
                           this.Documentcd == null ? "GEN_ID(DOCUMENTS_GEN,1)" : GetValueForInsert(this.Documentcd),
                           GetValueForInsert(this.Organizationcd),
                           GetValueForInsert(this.Registerusercd),
                           GetValueForInsert(this.Otvetstvusercd),
                           GetValueForInsert(this.Doctypeid),
                           GetValueForInsert(this.Docname),
                           GetValueForInsert(this.Doc_number),
                           GetValueForInsert(this.Doc_ser),
                           GetValueForInsert(this.Docdate, DateTimeType.Только_дата),
                           GetValueForInsert(this.Inputdate, DateTimeType.Дата_и_время),
                           GetValueForInsert(this.Outputdate, DateTimeType.Дата_и_время),
                           GetValueForInsert(this.Factdocumentdate, DateTimeType.Дата_и_время),
                           "NULL",  // DOCUMENTIDENTIFER, присваивается триггером
                           GetValueForInsert(this.Internaluseonly),
                           GetValueForInsert(this.Employee),
                           GetValueForInsert(this.Fromsuperior),
                           GetValueForInsert(this.Status),
                           GetValueForInsert(this.Reasonid));
                return s;
            }
        }
    }
}
