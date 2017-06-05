using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RexSimulator.Hardware.Rex;

namespace RexSimulatorGui
{
    class HardDriveController
    {
        enum State {Ready, Sending, Receiving };
        static int SIZE = 1024 * 1024 * 16; //16MB
        char[] disk = new char[SIZE];
        char[] buffer = new char[512];
        int count = 0;
        State curr_state = State.Ready;
        SerialIO mserial;
        HandShake curr_handshake;
        public HardDriveController(Stream stream, SerialIO mserial)
        {
            StreamReader reader = new StreamReader(stream);
            int read = reader.Read(disk, 0, SIZE);
            this.mserial = mserial;
            if (read != SIZE)
            {
                Console.WriteLine("unable to read byte");
                Console.Write(disk[SIZE - 1]);
            }
        }

        public void Receiver(object sender, SerialIO.SerialEventArgs e)
        {
            char c = (char)e.Data;
            if(c == '\n')
            {
                ProcessRequest();
                return;
            }
            buffer[count] = (char)e.Data;
            count++;
            
        }

        private void Ack()
        {
            curr_handshake.Ack();
            foreach(char c  in curr_handshake.ToString())
            {
                mserial.SendAsync(c);
            }
            mserial.SendAsync('\n');
        }

        private void ProcessRequest()
        {
            if(curr_state == State.Ready)
            {
                curr_handshake = new HandShake(buffer.ToString());
                if(curr_handshake.mode == 0) //read
                {
                    Ack();
                    curr_state = State.Sending;
                    SendData(curr_handshake.pos,curr_handshake.len);
                }
                else //write
                {
                    Ack();
                    curr_state = State.Receiving;
                }
            }else if(curr_state == State.Receiving)
            {
                WriteReceivedData();
            }else if(curr_state == State.Sending)
            {
                Console.WriteLine("ERROR: receve data while sending characters to WINIX");
            }
        }


        public void SendData(int read_pos, int read_len)
        {
            for (int i=read_pos; i < read_pos + read_len; i++)
            {
                mserial.SendAsync(disk[i]);
            }
            mserial.SendAsync('\n');
            curr_state = State.Ready;
            count = 0; //reset buffer;
            curr_handshake = null;
        }

        public void WriteReceivedData()
        {
            for(int i = curr_handshake.pos, j=0; i < count && j< count; j++,i++)
            {
                buffer[i] = buffer[j];
            }
            count = 0;
            curr_state = State.Ready;
            curr_handshake = null;
        }

        class HandShake
        {
            public byte flag;
            public byte mode;
            public int pos;
            public int len;
            private StringBuilder basestr;
            public HandShake(string str)
            {
                flag = Convert.ToByte(str[0]);
                mode = Convert.ToByte(str[1]);
                pos = Convert.ToInt32(str.Skip(2).Take(8).ToArray());
                if(mode == 0)//read
                {
                    len = Convert.ToInt32(str.Skip(10).Take(8).ToArray());
                }
                else
                {
                    len = -1;
                }
                basestr = new StringBuilder(str);
                
            }
            public void Ack()
            {
                basestr[0] = '1';
                
            }

            public override string ToString()
            {
                return basestr.ToString();
            }
        }
    }
}
