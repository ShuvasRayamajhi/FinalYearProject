﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegApp
{
    class StegDecode
    {
        public static string StegDecoding(Bitmap bitmp)
        {
            string decodedText = ""; //stores the decoded teg that will be returned
            int colour = 0; //index of colour unit from image
            int value = 0; //store the intger, converted from character to hide
            int height; //height of the image
            int width;  //width
            Color pixel; //stores the current pixel in the processing

            for (height = 0; height < bitmp.Height; height++) //go through all the height pixels of the image
            {
                for (width = 0; width < bitmp.Width; width++) //go through the width pixels
                {
                    int pix;
                    pixel = bitmp.GetPixel(width, height);  //get the pixels from the decoded image
                    for (pix = 0; pix < 3; pix++) //for every pixel, go through the RGB elements
                    {
                        try
                        {
                            switch (colour % 3)
                            {
                                case 0:
                                    value = value * 2 + pixel.R % 2; //get the result of pixel element Red % 2, then add a bit to right of current character value
                                    break;
                                case 1:
                                    value = value * 2 + pixel.G % 2; //adding one bit to right of the current character (value * 2)
                                    break;
                                case 2:
                                    value = value * 2 + pixel.B % 2; //the added bit which is "0" is then replaced with lowest significant bit(where the hidden data is) from each pixel just by adding; whilst encoding we subtracted. 
                                    break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Failed at decoding."); 
                        }
                        colour++; //keep going until colour unit gets to 8
                       
                        if (colour % 8 == 0) // check that 8 bits have been added. Then add the current character in the loop to the output text
                        {
                            try
                            {
                                if (value == 0) //if the value is 0 then it indicates it is the end because of we added 8 zeroes
                                    return decodedText; //return decoded text
                                char character = (char)value; //convert character value to character
                                decodedText += character.ToString(); //coventry characters back to string
                            }
                            catch
                            {
                                Console.WriteLine("Failed to return or convert text to string");
                            }
                        }
                    }
                }
            }
            return decodedText; //returnt the plain text
        }
    }
}
