using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDataReading.AircraftModel1;
using FlightDataEntitiesRT;
using System.Collections;
using FlightDataEntitiesRT.Decisions;

namespace FlightDataReading.AircraftModel1
{
    public class FlightDataReadingHandler : IFlightRawDataExtractor
    {
        private BinaryReader m_Reader = null;

        public FlightDataReadingHandler(BinaryReader reader)
        {
            this.m_Reader = reader;

            FlightBinaryDataDefinition definition = new FlightBinaryDataDefinition()
            {
                #region init
                HeaderDefinition = new FlightBinaryDataHeaderDefinition()
                {
                    BytesCount = 128,
                    Segments = new FlightBinaryDataContentSegmentDefinition[]
                  {
                       //文件头
                      new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 1,
                           DataTypeStr = DataTypeConverter.STRING,
                            SegmentName = "Year"},
                            new FlightBinaryDataContentSegmentDefinition(){
                                 BytesCount = 1,
                                  DataTypeStr = DataTypeConverter.STRING,
                                   SegmentName = "Month"},
                                   new FlightBinaryDataContentSegmentDefinition(){
                                       BytesCount = 1,
                                        DataTypeStr = DataTypeConverter.STRING,
                                         SegmentName = "Day"},
                                   new FlightBinaryDataContentSegmentDefinition(){
                                       BytesCount = 1,
                                        DataTypeStr = DataTypeConverter.STRING,
                                         SegmentName = "AircraftModel"},
                                         new FlightBinaryDataContentSegmentDefinition(){
                                              BytesCount = 1,
                                               DataTypeStr = DataTypeConverter.STRING,
                                                SegmentName = "VSTOL"}, //起落
                                         new FlightBinaryDataContentSegmentDefinition(){
                                              BytesCount = 4,
                                               DataTypeStr = DataTypeConverter.INT32,
                                                SegmentName = "FlightSubject"},
                        new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 2,
                            DataTypeStr = DataTypeConverter.STRING,
                            SegmentName = "Others"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, // 估计是8位而不是12位 BytesCount = 12,
                                 DataTypeStr = DataTypeConverter.INT32,
                                 SegmentName = "AircraftNumber"},
                                 new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 2,
                                     DataTypeStr = DataTypeConverter.STRING, SegmentName = "FileNumber"},
                                     new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 32,
                                          DataTypeStr = DataTypeConverter.STRING, SegmentName = "EngineCareer"} 
                  }
                },

                FrameDefinition = new FlightBinaryDataContentFrameDefinition()
                {
                    BytesCount = 1024,
                    Segments = new FlightBinaryDataContentSegmentDefinition[]
                        { 
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Et"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //一行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "aT"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "DR"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "GS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dy"},
                                 //2行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "EW"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CN"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "T6L"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "T6R"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "NHR"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "NHL"},
                                 //3行                                 

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Wx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Vi"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "M"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Tt"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZH"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "FY"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //4行                                 

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //5行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "aT"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "DR"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "GS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dy"},
                                 //6行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "NS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //7行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Nz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Nx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Wy"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "HG"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Vy"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Hp"},
                                 //8行
                                 
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ED"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //9行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Wx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "aT"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "DR"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "GS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dy"},
                                 //10行
                                 
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KG15->1", BitsDefinition = new FlightBitDataContentSegmentDefinition[] 
                                 { 
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG1", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG2", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG3", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG4", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG5", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG6", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG7", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG8", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG9", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG10", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG11", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG12", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG13", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG14", DataTypeStr = "Int32"},
                                     new FlightBitDataContentSegmentDefinition(){ BitsCount = 1, SegmentName = "KG15", DataTypeStr = "Int32"},
                                 },
                            },
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //11行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "T6L"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CN"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "T6R"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "YD"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "NHL"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "NHR"},
                                 //12行
                                 
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ND"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Tt"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //13行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "aT"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "DR"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "GS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Dy"},
                                 //14行
                                 
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KG17"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KZB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "KCB"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ZS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx1"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fy2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                                 //15行

                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Fx2"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "CS"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Ny"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Nz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Nx"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Wz"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "HG"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Vy"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "ID"},
                            new FlightBinaryDataContentSegmentDefinition(){ BytesCount = 4, DataTypeStr = DataTypeConverter.FLOAT,
                                 SegmentName = "Hp"},
                                 //16行
                        }
                }

                #endregion
            };

            this.Definition = definition;
        }

        public FlightDataHeader ReadHeader()
        {
            if (this.m_Reader == null || this.m_Reader.BaseStream.CanRead == false)
            {
                throw new IOException("无法读取数据源文件。");
            }

            //BufferedStream stream = new BufferedStream(new FileStream(this.FileInfo.FullName, FileMode.Open));
            //m_stream = stream;
            //using (m_Reader = new BinaryReader(stream))
            //{
            //try
            //{
            return DoReadHeaderCore();
            //    }
            //    catch (Exception e)
            //    {//TODO: LOG it
            //        if (this.m_stream != null)
            //        {
            //            this.m_stream.Close();
            //            this.m_stream.Dispose();
            //            this.m_stream = null;
            //        }

            //        throw e;
            //    }
            //}

            //if (this.m_stream != null)
            //{
            //    this.m_stream.Close();
            //    this.m_stream.Dispose();
            //    this.m_stream = null;
            //}
        }

        private FlightDataHeader DoReadHeaderCore()
        {
            FlightDataHeader header = new FlightDataHeader();

            List<FlightDataContentSegment> segments = new List<FlightDataContentSegment>();

            m_Reader.BaseStream.Position = 0;
            foreach (var seg in this.Definition.HeaderDefinition.Segments)
            {
                FlightDataContentSegment content = new FlightDataContentSegment()
                {
                    DataTypeStr = seg.DataTypeStr,
                    SegmentName = seg.SegmentName
                };

                if (seg.DataTypeStr == DataTypeConverter.FLOAT)
                {
                    float floatVal = m_Reader.ReadSingle();
                    content.Value = floatVal;// floatVal.ToString();
                }
                else if (seg.DataTypeStr == DataTypeConverter.INT32)
                {
                    int intVal = m_Reader.ReadInt32();
                    content.Value = intVal;// intVal.ToString();
                }
                else if (seg.DataTypeStr == DataTypeConverter.LONG)
                {
                    long longVal = m_Reader.ReadInt64();
                    content.Value = longVal;// longVal.ToString();
                }
                else if (seg.DataTypeStr == DataTypeConverter.DATETIME)
                {
                    content.Value = 0;// string.Empty;
                }
                else
                {
                    byte[] strs = m_Reader.ReadBytes(seg.BytesCount);
                    content.Value = 0;// new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(strs));
                }
                segments.Add(content);
            }

            m_Reader.BaseStream.Position = this.Definition.HeaderDefinition.BytesCount;

            header.Segments = segments.ToArray();
            return header;
        }

        public void Read()
        {
            if (this.m_Reader == null || this.m_Reader.BaseStream.CanRead == false)
            {
                throw new IOException("无法读取数据源文件。");
            }

            //BufferedStream stream = new BufferedStream(new FileStream(this.FileInfo.FullName, FileMode.Open));
            //m_stream = stream;
            //using (m_Reader = new BinaryReader(stream))
            //{
            //    try
            //    {
            DoCore();
            //    }
            //    catch (Exception e)
            //    {//TODO: LOG it
            //        if (this.m_stream != null)
            //        {
            //            this.m_stream.Close();
            //            this.m_stream.Dispose();
            //            this.m_stream = null;
            //        }

            //        throw e;
            //    }
            //}

            //if (this.m_stream != null)
            //{
            //    this.m_stream.Close();
            //    this.m_stream.Dispose();
            //    this.m_stream = null;
            //}
        }

        private void DoCore()
        {
            DateTime start = DateTime.Now;

            m_Reader.BaseStream.Position = this.Definition.HeaderDefinition.BytesCount;

            List<FlightDataContentFrame> frames = new List<FlightDataContentFrame>();

            while (m_Reader.BaseStream.Length > m_Reader.BaseStream.Position + 1)
            {
                FlightDataReading.AircraftModel1.FlightDataContentFrame frame = new FlightDataContentFrame();
                long current = m_Reader.BaseStream.Position;

                List<FlightDataContentSegment> segments = new List<FlightDataContentSegment>();
                foreach (var seg in this.Definition.FrameDefinition.Segments)
                {
                    FlightDataContentSegment content = new FlightDataContentSegment()
                    {
                        DataTypeStr = seg.DataTypeStr,
                        SegmentName = seg.SegmentName
                    };

                    if (seg.DataTypeStr == DataTypeConverter.FLOAT)
                    {
                        float floatVal = m_Reader.ReadSingle();
                        content.Value = floatVal;// floatVal.ToString();
                    }
                    else if (seg.DataTypeStr == DataTypeConverter.INT32)
                    {
                        int intVal = m_Reader.ReadInt32();
                        content.Value = intVal;// intVal.ToString();
                    }
                    else if (seg.DataTypeStr == DataTypeConverter.LONG)
                    {
                        long longVal = m_Reader.ReadInt64();
                        content.Value = longVal;// longVal.ToString();
                    }
                    else if (seg.DataTypeStr == DataTypeConverter.DATETIME)
                    {
                        content.Value = 0;// string.Empty;
                    }
                    else
                    {
                        byte[] strs = m_Reader.ReadBytes(seg.BytesCount);
                        content.Value = 0;// new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(strs));
                    }
                    segments.Add(content);
                }

                var filtered = segments.Where(
                     new Func<FlightDataContentSegment, bool>(
                         delegate(FlightDataContentSegment seg1)
                         {
                             if (seg1 == null || string.IsNullOrEmpty(seg1.SegmentName) || seg1.SegmentName == "ID")
                                 return false;
                             return true;
                         }));

                frame.Segments = filtered.ToArray();//segments.ToArray();
                frames.Add(frame);

                if (this.m_Reader.BaseStream.Position != current + this.Definition.FrameDefinition.BytesCount)
                {
                    if (this.m_Reader.BaseStream.Position + 1 >= this.m_Reader.BaseStream.Length)
                        break;
                    this.m_Reader.BaseStream.Position = current + this.Definition.FrameDefinition.BytesCount;
                }
            }

            this.Frames = frames;

            return;

            //PHYHeader header = PHYHelper.ReadPHYHeader(m_Reader);
            //int secondCount = PHYHelper.GetFlyParamSeconds(header);

            //Task task1 = this.DoAddFlightRecordAsync(header);

            //string aircraftModel = header.AircrfName;

            ////获取需要读取的飞参列表
            //List<FlyParameter> needReadParams = FlyParameter.Parameters.Values.ToList();

            //Dictionary<string, FlyParameter> paramMap = new Dictionary<string, FlyParameter>();
            //foreach (FlyParameter fp in needReadParams)
            //    paramMap.Add(fp.ID, fp);

            //this.InitLevel2Entities(paramMap, secondCount);

            //for (int i = 1; i <= secondCount; i++)
            //{
            //    //读取当前秒的飞参值
            //    foreach (FlyParameter fp in needReadParams)
            //    {
            //        float[] values = PHYHelper.ReadFlyParameter(m_Reader, i, header, fp);
            //        FlightDataEntities.FlightRawData dt
            //            = new FlightDataEntities.FlightRawData() { ParameterID = fp.ID, Second = i, Values = values };
            //        this.PutRawDataToMongoDB(dt);

            //        FlightRecordEntity entity = new FlightRecordEntity() { FlightSecond = i, ParameterID = fp.ID, Values = values };
            //        this.PutEntityToLevel2Mentities(entity, secondCount);
            //    }
            //}

            //Task task2 =
            //    Task.Run(new Action(delegate()
            //{
            //    this.FlushDataToMongoDB(secondCount);
            //}));
            ////一边入库一边处理Fault判定

            //if (task1 != null)
            //    task1.Wait();
            //if (task2 != null)
            //    task2.Wait();
        }

        //private void InitLevel2Entities(Dictionary<string, FlightDataReading.AircraftModel1.FlyParameter> paramMap, int secondCount)
        //{
        //    m_reducedPointsMap = new Dictionary<string, List<FlightRecordPoint>>();
        //    m_dataHelperMap = new Dictionary<string, Level2DataHelper>();
        //    m_level2DataMap = new Dictionary<string, Level2FlightRecord>();

        //    //初始化暂定使用一层
        //    foreach (string key in paramMap.Keys)
        //    {
        //        m_reducedPointsMap.Add(key, new List<FlightRecordPoint>());
        //        m_dataHelperMap.Add(key,
        //            new Level2DataHelper() { MaxValue = float.MinValue, MinValue = float.MaxValue, Count = 0, SumValue = 0 });
        //    }
        //}

        //private Dictionary<string, Level2DataHelper> m_dataHelperMap = null;
        //private Dictionary<string, Level2FlightRecord> m_level2DataMap = null;
        //private Dictionary<string, List<FlightRecordPoint>> m_reducedPointsMap = null;
        ////new Dictionary<string, List<FlightRecordPoint>>();

        //class Level2DataHelper
        //{
        //    public float MinValue { get; set; }
        //    public float MaxValue { get; set; }
        //    public decimal SumValue { get; set; }
        //    public int Count { get; set; }
        //}

        //private void PutEntityToLevel2Mentities(FlightRecordEntity entity, int lastSecond)
        //{//TODO: 处理修改Level2、精简和……
        //    if (entity == null || string.IsNullOrEmpty(entity.ParameterID)
        //        || this.m_level2DataMap == null // || !this.m_level2DataMap.ContainsKey(entity.ParameterID)
        //        || !m_reducedPointsMap.ContainsKey(entity.ParameterID) || entity.ValueCount == 0)
        //        return;

        //    if (m_dataHelperMap.ContainsKey(entity.ParameterID))
        //    {
        //        var helper = m_dataHelperMap[entity.ParameterID];
        //        helper.Count++;
        //        helper.MinValue = Math.Min(helper.MinValue, entity.MinValue);
        //        helper.MaxValue = Math.Max(helper.MaxValue, entity.MaxValue);
        //        helper.SumValue += Convert.ToDecimal(entity.Values.Sum());
        //    }

        //    //var level2 = this.m_level2DataMap[entity.ParameterID];
        //    if (entity.ValueCount == 1
        //        || (entity.MinValue == entity.AvgValue && entity.MaxValue == entity.AvgValue))
        //    {//只有一个点
        //        //或者多个点平均值最大值最小值全等
        //        //可以认为曲线是平缓的，只记录一个点即可
        //        FlightRecordPoint point = new FlightRecordPoint()
        //        {
        //            Second = entity.FlightSecond,
        //            MillSec = 0,
        //            Value = entity.Values[0]
        //        };
        //        m_reducedPointsMap[entity.ParameterID].Add(point);

        //        if (entity.ValueCount > 1 && entity.FlightSecond == lastSecond)
        //        {//如果超过一个点并且是最后一秒，则再记录最后一个点，否则线可能画不出来
        //            FlightRecordPoint point2 = new FlightRecordPoint()
        //            {
        //                Second = entity.FlightSecond,
        //                MillSec = 999,//最后一毫秒
        //                Value = entity.Values[entity.ValueCount - 1]
        //            };
        //            m_reducedPointsMap[entity.ParameterID].Add(point2);
        //        }

        //        return;
        //    }

        //    //多个点的值有不同的情况，全部入库
        //    for (int i = 0; i < entity.ValueCount; i++)
        //    {
        //        FlightRecordPoint point3 = new FlightRecordPoint()
        //        {
        //            Second = entity.FlightSecond,
        //            MillSec = Convert.ToInt32(Decimal.Round(
        //                Convert.ToDecimal(1000.0 * (i / entity.ValueCount)))),
        //            Value = entity.Values[entity.ValueCount - 1]
        //        };
        //        m_reducedPointsMap[entity.ParameterID].Add(point3);
        //    }
        //}

        //private void FlushDataToMongoDB(int lastSecond)
        //{
        //    //先处理精简后的Level2数据
        //    foreach (string key in this.m_reducedPointsMap.Keys)
        //    {
        //        var list = m_reducedPointsMap[key];
        //        Level2FlightRecord record = new Level2FlightRecord() { ParameterID = key };

        //        var helper = m_dataHelperMap[key];
        //        record.Count = helper.Count;
        //        record.StartSecond = 0;
        //        record.EndSecond = lastSecond;
        //        record.MinValue = helper.MinValue;
        //        record.MaxValue = helper.MaxValue;
        //        record.AvgValue = Convert.ToSingle(helper.SumValue / helper.Count);

        //        record.Level1FlightRecords = null;//DEBUG//list.ToArray();

        //        m_level2DataMap.Add(record.ParameterID, record);
        //    }

        //    MongoClient dbClient = new MongoClient(this.MongoDBConnectionString);

        //    MongoServer dbServer = dbClient.GetServer();

        //    dbServer.Connect();
        //    MongoDatabase db = dbServer.GetDatabase(this.PreSetAircraftModelName);

        //    MongoCollection<Level2FlightRecord> level2Collection
        //        = db.GetCollection<Level2FlightRecord>(AircraftCollections.Level2FlightRecord);

        //    var start = DateTime.Now;
        //    int counter = 0;
        //    foreach (var oneLine in m_level2DataMap.Values)
        //    {
        //        counter++;
        //        level2Collection.Insert(oneLine);
        //        if (counter % 10 == 0)
        //        {
        //            System.Diagnostics.Debug.WriteLine(string.Format("inputed level2 data count: {0}", counter));
        //        }
        //    }
        //    var end = DateTime.Now;
        //    System.Diagnostics.Debug.WriteLine(string.Format("counter: {0}", counter));
        //    System.Diagnostics.Debug.WriteLine(string.Format(
        //        "total mongodb level2 insert millsec: {0}", end.Subtract(start).TotalMilliseconds));

        //    foreach (string paramId in m_reducedPointsMap.Keys)
        //    {
        //        var query = from one in level2Collection.AsQueryable<Level2FlightRecord>()
        //                    where one.ParameterID == paramId
        //                    select one;

        //        if (query != null)
        //        {
        //            //
        //        }

        //        //    Query<Level2FlightRecord>.EQ(
        //        //        e => e.ParameterID, paramId);
        //        //Level2FlightRecord rec = level2Collection.FindOne(query);
        //    }
        //    var end2 = DateTime.Now;
        //    System.Diagnostics.Debug.WriteLine(string.Format(
        //        "total mongodb level2 select millsec: {0}", end2.Subtract(end).TotalMilliseconds));

        //    dbServer.Disconnect();

        //    //DEBUG: 暂时不处理入库
        //}

        //private void PutRawDataToMongoDB(FlightDataEntities.FlightRawData dt)
        //{
        //    //DEBUG: 暂时不处理入库
        //}

        //private Task DoAddFlightRecordAsync(FlightDataReading.AircraftModel1.PHYHeader header)
        //{
        //    return null;
        //}


        //private Stream m_stream = null;
        //private object m_syncRoot = new object();

        //private string m_filePath = string.Empty;

        //public string FilePath
        //{
        //    get { return m_filePath; }
        //    set
        //    {
        //        m_filePath = value;
        //        if (string.IsNullOrEmpty(this.m_filePath))
        //            this.m_fileInfo = null;
        //        else this.m_fileInfo = new FileInfo(this.m_filePath);
        //    }
        //}

        //private FileInfo m_fileInfo = null;

        //public FileInfo FileInfo
        //{
        //    get
        //    {
        //        return m_fileInfo;
        //    }
        //}

        //public string MongoDBConnectionString { get; set; }

        //public string PreSetAircraftModelName { get; set; }

        public FlightBinaryDataDefinition Definition { get; set; }

        public List<FlightDataContentFrame> Frames { get; set; }

        public FlightDataEntitiesRT.FlightDataHeader GetHeader()
        {
            int secondCount = 0;
            long segmentLength = this.m_Reader.BaseStream.Length - this.Definition.HeaderDefinition.BytesCount;

            if (segmentLength % this.Definition.FrameDefinition.BytesCount == 0)
            {//刚好整数，那么最后一秒是数据要去掉的
                int sec = Convert.ToInt32(segmentLength / this.Definition.FrameDefinition.BytesCount);
                secondCount = sec - 1;
            }
            else
            {//事实上去掉了两秒？
                int sec = Convert.ToInt32(segmentLength / this.Definition.FrameDefinition.BytesCount);
                secondCount = sec - 1;
            }

            var header = this.ReadHeader();

            float[] ns = this.ForceReadContentParameterValues("NS");
            float[] ew = this.ForceReadContentParameterValues("EW");

            //TODO: 转换赋值
            return new FlightDataEntitiesRT.FlightDataHeader()
            {
                FlightDate = DateTime.Now,//debug
                FlightSeconds = secondCount,
                Latitudes = ew,
                Longitudes = ns,
                Description = header.ToString()
            };
        }

        private float[] ForceReadContentParameterValues(string parameterID)
        {
            List<float> resultValues = new List<float>();
            DateTime start = DateTime.Now;
            //复位
            m_Reader.BaseStream.Position = this.Definition.HeaderDefinition.BytesCount;

            int startPoint = 0;
            FlightBinaryDataContentSegmentDefinition paramDef = null;
            int startStep = this.Definition.FrameDefinition.BytesCount;

            foreach (var preSeg in this.Definition.FrameDefinition.Segments)
            {
                if (preSeg.SegmentName != parameterID)
                    startPoint += preSeg.BytesCount;
                else
                {
                    paramDef = preSeg;
                    break;
                }
            }

            //用于移动到Start
            m_Reader.BaseStream.Position += startPoint;

            while (m_Reader.BaseStream.Length > m_Reader.BaseStream.Position + 1)
            {
                float val = m_Reader.ReadSingle();
                resultValues.Add(val);
                m_Reader.BaseStream.Position += (startStep - paramDef.BytesCount);//已经读了一个param，要位移就要去除它的位数
            }

            m_Reader.BaseStream.Position = this.Definition.HeaderDefinition.BytesCount;

            return resultValues.ToArray();
        }

        public TimeSpan TestSpan1
        {
            get;
            set;
        }

        public TimeSpan TestSpan2
        {
            get;
            set;
        }

        public TimeSpan TestSpan3
        {
            get;
            set;
        }

        public ParameterRawData[] GetDataBySecond(int second)
        {
            if (this.m_Reader == null || this.m_Reader.BaseStream.CanRead == false)
            {
                throw new IOException("无法读取数据源文件。");
            }

            if (this.Definition == null)
                return null;

            int startPos = this.Definition.HeaderDefinition.BytesCount +
                (second * this.Definition.FrameDefinition.BytesCount);

            if (this.m_Reader.BaseStream.Position != startPos)
                this.m_Reader.BaseStream.Position = startPos;

            BitArray bitArray = new BitArray(this.m_Reader.ReadBytes(1024));

            if (this.m_Reader.BaseStream.Position != startPos)
                this.m_Reader.BaseStream.Position = startPos;//复位

            FlightDataReading.AircraftModel1.FlightDataContentFrame frame = new FlightDataContentFrame();
            long current = m_Reader.BaseStream.Position;

            List<FlightDataContentSegment> segments = new List<FlightDataContentSegment>();
            foreach (var seg in this.Definition.FrameDefinition.Segments)
            {
                if (seg == null)
                    continue;

                if ((string.IsNullOrEmpty(seg.SegmentName) ||
                    seg.SegmentName.Equals("(NULL)") || seg.SegmentName.Equals("ID"))
                    && (seg.BitsDefinition == null || seg.BitsDefinition.Length == 0))
                {
                    m_Reader.BaseStream.Position += seg.BytesCount;
                    continue;
                }

                var testInt = m_Reader.ReadInt32();
                m_Reader.BaseStream.Position -= 4;

                if (seg.BitsDefinition != null && seg.BitsDefinition.Length > 0)
                {
                    this.ReadComplexSegment(segments, seg, bitArray);
                }
                else
                {
                    ReadSimpleSegment(segments, seg, bitArray);
                }
            }
            var result = from se in segments
                         group se by se.SegmentName into gse
                         select gse;

            var result2 = from g in result
                          select new ParameterRawData()
                          {
                              ParameterID = g.Key,
                              Second = second,
                              Values =
                                  (from one in g
                                   select Convert.ToSingle(one.Value)).ToArray()
                          };
            return result2.ToArray();
        }

        private void ReadComplexSegment(List<FlightDataContentSegment> segments,
            FlightBinaryDataContentSegmentDefinition seg, BitArray bitArray)
        {
            var single = m_Reader.ReadSingle();
            var toInt = (int)single;
            List<int> bitValues = new List<int>();

            for (int i = 0; i < 16; i++)
            {
                int bitValue = BaseFunction.GetBit(toInt, i);
                bitValues.Add(bitValue);
            }

            for (int j = 0; j < Math.Min(seg.BitsDefinition.Length, bitValues.Count); j++)
            {
                var seg2 = seg.BitsDefinition[j];
                FlightDataContentSegment resultSeg = new FlightDataContentSegment()
                {
                    DataTypeStr = seg2.DataTypeStr,
                    SegmentName = seg2.SegmentName,
                };

                if (seg2.BitsCount == 1)
                {
                    resultSeg.Value = bitValues[j];
                }
                segments.Add(resultSeg);
            }
        }

        private void ReadSimpleSegment(List<FlightDataContentSegment> segments,
            FlightBinaryDataContentSegmentDefinition seg, BitArray bitArray)
        {
            var s3 = DateTime.Now;

            FlightDataContentSegment content = new FlightDataContentSegment()
            {
                DataTypeStr = seg.DataTypeStr,
                SegmentName = seg.SegmentName
            };

            //每个Index长度是4（byteCount）
            long currentPos = m_Reader.BaseStream.Position;
            if (seg.DataTypeStr == DataTypeConverter.FLOAT)
            {
                var s1 = DateTime.Now;
                float floatVal = m_Reader.ReadSingle();
                var s2 = DateTime.Now;
                TestSpan1 += s2.Subtract(s1);
                if (currentPos + seg.BytesCount != m_Reader.BaseStream.Position)
                    m_Reader.BaseStream.Position = currentPos + seg.BytesCount;
                content.Value = floatVal;// floatVal.ToString();
                TestSpan2 += DateTime.Now.Subtract(s2);

            }
            else if (seg.DataTypeStr == DataTypeConverter.INT32)
            {
                int intVal = m_Reader.ReadInt32();
                content.Value = intVal;// intVal.ToString();
                if (currentPos + seg.BytesCount != m_Reader.BaseStream.Position)
                    m_Reader.BaseStream.Position = currentPos + seg.BytesCount;
            }
            else if (seg.DataTypeStr == DataTypeConverter.LONG)
            {
                long longVal = m_Reader.ReadInt64();
                content.Value = longVal;// longVal.ToString();
                if (currentPos + seg.BytesCount != m_Reader.BaseStream.Position)
                    m_Reader.BaseStream.Position = currentPos + seg.BytesCount;
            }
            else if (seg.DataTypeStr == DataTypeConverter.DATETIME)
            {
                content.Value = 0;// string.Empty;
                if (currentPos + seg.BytesCount != m_Reader.BaseStream.Position)
                    m_Reader.BaseStream.Position = currentPos + seg.BytesCount;
            }
            else
            {
                byte[] strs = m_Reader.ReadBytes(seg.BytesCount);
                content.Value = 0;// new string(System.Text.Encoding.GetEncoding("ASCII").GetChars(strs));
                if (currentPos + seg.BytesCount != m_Reader.BaseStream.Position)
                    m_Reader.BaseStream.Position = currentPos + seg.BytesCount;
            }
            TestSpan3 += DateTime.Now.Subtract(s3);

            segments.Add(content);
        }


        public void Close()
        {
            if (m_Reader != null)
                this.m_Reader.Dispose();
        }


        public FlightDataEntitiesRT.Decisions.Decision[] GetFaultDecisions()
        {
            var decisions = new FlightDataEntitiesRT.Decisions.Decision[]{
                //new FlightDataEntitiesRT.Decisions.Decision(){
                //     LastTime = 1, Conditions = new FlightDataEntitiesRT.Decisions.SubCondition[]{
                //         new FlightDataEntitiesRT.Decisions.CompareSubCondition(){ ParameterID = "T6R",
                //              ParameterValue = 630 , Operator = FlightDataEntitiesRT.Decisions.CompareOperator.GreaterThan},
                //              new FlightDataEntitiesRT.Decisions.CompareSubCondition(){ ParameterID = "KG14",
                //              ParameterValue = 1 , Operator = FlightDataEntitiesRT.Decisions.CompareOperator.Equal},
                //     }},
                     new FlightDataEntitiesRT.Decisions.Decision(){
                     LastTime = 10, Conditions = new FlightDataEntitiesRT.Decisions.SubCondition[]{
                         new FlightDataEntitiesRT.Decisions.CompareSubCondition(){ ParameterID = "Vi",
                              ParameterValue = 100 , Operator = FlightDataEntitiesRT.Decisions.CompareOperator.GreaterThan},
                              new FlightDataEntitiesRT.Decisions.CompareSubCondition(){ ParameterID = "NHL",
                              ParameterValue = 99 , Operator = FlightDataEntitiesRT.Decisions.CompareOperator.GreaterThan},
                     }}
            };

            foreach (var decision in decisions)
            {
                Queue<SubCondition> conds = new Queue<SubCondition>();
                foreach (var sub in decision.Conditions)
                {
                    conds.Enqueue(sub);
                }
                while (conds.Count > 0)
                {
                    var s = conds.Dequeue();
                    s.RootDecision = decision;

                    if (s.SubConditions != null)
                    {
                        foreach (var sub in s.SubConditions)
                        {
                            conds.Enqueue(sub);
                        }
                    }
                }
            }

            return decisions;
        }

        public DateTime ParseDate(string p)
        {
            if (!string.IsNullOrEmpty(p) && p.Length >= 8)
            {
                string sub = p.Substring(2, 6);
                DateTime dt = DateTime.ParseExact(sub, "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                //string yearStr = sub.Substring(0, 2);
                //string monthStr = sub.Substring(2, 2);
                //string dayStr = sub.Substring(4, 2);

                //DateTime dt2 = new DateTime(2000 + Convert.ToInt32(yearStr), 
                //    Convert.ToInt32(monthStr), Convert.ToInt32(dayStr));

                return dt;
                //return dt2;
            }

            return DateTime.Now;
        }

        public string ParseAircraftNumber(string p)
        {
            if (!string.IsNullOrEmpty(p) && p.Length >= 2)
            {
                return p.Substring(0, 2);
            }

            return string.Empty;
        }
    }
}
