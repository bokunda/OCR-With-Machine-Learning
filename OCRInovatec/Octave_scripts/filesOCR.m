## Copyright (C) 2018 inovatec-user
## 
## This program is free software: you can redistribute it and/or modify it
## under the terms of the GNU General Public License as published by
## the Free Software Foundation, either version 3 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful, but
## WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program.  If not, see
## <https://www.gnu.org/licenses/>.

## -*- texinfo -*- 
## @deftypefn {} {@var{retval} =} filesOCR (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: inovatec-user <inovatec-user@DESKTOP-760TGTJ>
## Created: 2018-08-16

function retval = filesOCR ()

%ID
id1 ={'1.jpg.txt', '10.png.txt', '11.jpg.txt', '12.jpg.txt', '13.jpg.txt', '14.jpg.txt', '15.jpg.txt', '16.jpg.txt', '18.jpg.txt', '19.png.txt', '2.jpg.txt', '20.jpg.txt', '21.jpeg.txt', '22.jpg.txt', '23.png.txt', '25.jpg.txt', '26.jpg.txt', '27.jpg.txt', '28.jpg.txt', '29.jpg.txt', '3.png.txt', '30.jpg.txt', '31.jpg.txt', '32.jpg.txt', '33.jpg.txt', '4.jpg.txt', '5.jpg.txt', '6.jpg.txt', '7.jpg.txt', '8.jpg.txt', '9.jpg.txt'};
%IDTEXT
id2 = {'1.jpg.txt', '10.png.txt', '100.jpg.txt', '101.jpg.txt', '102.jpg.txt', '103.jpg.txt', '104.jpg.txt', '105.jpg.txt', '106.jpg.txt', '107.jpg.txt', '108.jpg.txt', '109.jpg.txt', '11.jpg.txt', '110.jpg.txt', '111.jpg.txt', '112.jpg.txt', '113.jpg.txt', '114.jpg.txt', '115.jpg.txt', '116.jpg.txt', '117.jpg.txt', '118.jpg.txt', '119.jpg.txt', '12.jpg.txt', '120.jpg.txt', '121.jpg.txt', '122.jpg.txt', '123.jpg.txt', '124.jpg.txt', '125.jpg.txt', '126.jpg.txt', '127.jpg.txt', '128.jpg.txt', '129.jpg.txt', '13.jpg.txt', '130.png.txt', '131.jpg.txt', '132.jpg.txt', '133.jpg.txt', '134.jpg.txt', '135.jpg.txt', '136.jpg.txt', '137.jpg.txt', '138.png.txt', '139.jpg.txt', '14.jpg.txt', '140.jpg.txt', '141.jpg.txt', '142.jpg.txt', '143.png.txt', '144.jpg.txt', '145.jpg.txt', '146.jpg.txt', '147.jpg.txt', '148.jpg.txt', '149.gif.txt', '15.jpg.txt', '150.jpg.txt', '151.jpg.txt', '152.jpg.txt', '153.jpg.txt', '154.jpg.txt', '155.jpg.txt', '156.jpg.txt', '157.jpg.txt', '158.jpg.txt', '159.jpg.txt', '16.jpg.txt', '160.jpg.txt', '161.jpg.txt', '162.jpg.txt', '163.png.txt', '164.jpg.txt', '165.jpg.txt', '166.jpg.txt', '167.jpg.txt', '168.gif.txt', '169.jpg.txt', '17.jpg.txt', '170.gif.txt', '171.jpg.txt', '172.png.txt', '173.jpg.txt', '174.png.txt', '176.jpg.txt', '177.jpg.txt', '18.jpg.txt', '19.png.txt', '2.jpg.txt', '20.jpg.txt', '21.jpeg.txt', '22.jpg.txt', '23.jpg.txt', '24.png.txt', '25.jpg.txt', '26.jpg.txt', '27.jpg.txt', '28.jpg.txt', '29.jpg.txt', '3.png.txt', '30.jpg.txt', '31.jpg.txt', '32.jpg.txt', '33.jpg.txt', '34.jpg.txt', '35.jpg.txt', '36.jpg.txt', '37.jpg.txt', '38.jpg.txt', '39.jpg.txt', '4.jpg.txt', '40.jpg.txt', '41.jpg.txt', '42.jpg.txt', '43.jpg.txt', '44.jpg.txt', '45.jpg.txt', '46.jpg.txt', '47.jpg.txt', '48.jpg.txt', '49.jpg.txt', '5.jpg.txt', '50.jpg.txt', '51.jpg.txt', '52.jpg.txt', '53.jpg.txt', '54.jpg.txt', '55.jpg.txt', '56.jpg.txt', '57.jpg.txt', '58.jpg.txt', '59.jpg.txt', '6.jpg.txt', '60.jpg.txt', '61.jpg.txt', '62.jpg.txt', '63.jpg.txt', '64.jpg.txt', '65.jpg.txt', '66.jpg.txt', '67.jpg.txt', '68.jpg.txt', '69.jpg.txt', '7.jpg.txt', '70.jpg.txt', '71.jpg.txt', '72.jpg.txt', '73.jpg.txt', '74.png.txt', '75.jpg.txt', '76.jpg.txt', '77.jpg.txt', '78.jpg.txt', '79.jpg.txt', '8.jpg.txt', '80.jpg.txt', '81.png.txt', '82.jpg.txt', '83.jpg.txt', '84.jpg.txt', '85.jpg.txt', '86.jpg.txt', '87.jpg.txt', '88.jpg.txt', '89.jpg.txt', '9.jpg.txt', '90.jpg.txt', '91.jpg.txt', '92.jpg.txt', '93.jpg.txt', '94.jpg.txt', '95.jpg.txt', '96.jpg.txt', '97.jpg.txt', '98.jpg.txt', '99.jpg.txt'};
#documents
text = {'1.png.txt', '10.jpg.txt', '100.jpg.txt', '101.jpg.txt', '102.jpeg.txt', '103.jpg.txt', '104.jpg.txt', '105.jpg.txt', '106.jpg.txt', '107.jpg.txt', '108.png.txt', '109.jpg.txt', '11.jpg.txt', '110.jpg.txt', '111.jpg.txt', '112.jpg.txt', '113.jpg.txt', '114.jpg.txt', '115.jpg.txt', '116.jpg.txt', '117.jpg.txt', '118.jpeg.txt', '119.png.txt', '12.jpg.txt', '120.png.txt', '121.jpg.txt', '122.png.txt', '123.jpg.txt', '124.jpg.txt', '125.jpg.txt', '126.png.txt', '127.png.txt', '128.jpg.txt', '129.jpg.txt', '13.jpg.txt', '130.png.txt', '131.jpg.txt', '132.jpg.txt', '133.jpg.txt', '134.png.txt', '135.png.txt', '136.jpg.txt', '137.jpg.txt', '138.jpg.txt', '139.png.txt', '14.jpg.txt', '140.png.txt', '141.jpg.txt', '142.jpg.txt', '143.jpg.txt', '144.png.txt', '145.png.txt', '146.jpg.txt', '147.jpg.txt', '148.png.txt', '149.jpg.txt', '15.jpg.txt', '150.png.txt', '151.jpeg.txt', '152.jpg.txt', '153.png.txt', '154.png.txt', '155.png.txt', '156.jpg.txt', '157.jpg.txt', '158.png.txt', '159.png.txt', '16.jpg.txt', '160.png.txt', '161.jpg.txt', '162.gif.txt', '163.jpg.txt', '164.jpg.txt', '165.jpg.txt', '166.jpg.txt', '167.png.txt', '168.jpg.txt', '169.jpg.txt', '17.jpg.txt', '170.png.txt', '171.png.txt', '172.gif.txt', '173.jpg.txt', '174.jpg.txt', '175.jpg.txt', '176.png.txt', '177.png.txt', '178.jpg.txt', '179.png.txt', '18.jpg.txt', '180.jpg.txt', '181.png.txt', '182.jpg.txt', '183.jpg.txt', '184.jpg.txt', '185.jpg.txt', '186.jpg.txt', '187.jpg.txt', '188.jpg.txt', '189.jpg.txt', '19.png.txt', '190.jpg.txt', '191.png.txt', '192.gif.txt', '193.jpg.txt', '194.jpg.txt', '195.gif.txt', '196.png.txt', '197.jpg.txt', '198.jpg.txt', '199.jpg.txt', '2.jpg.txt', '20.gif.txt', '200.jpg.txt', '21.png.txt', '22.jpg.txt', '23.jpg.txt', '24.jpg.txt', '25.png.txt', '26.png.txt', '27.png.txt', '28.jpg.txt', '29.png.txt', '3.jpg.txt', '30.jpg.txt', '31.jpg.txt', '32.png.txt', '33.png.txt', '34.jpg.txt', '35.png.txt', '36.png.txt', '37.jpg.txt', '38.png.txt', '39.jpg.txt', '4.jpg.txt', '40.jpg.txt', '41.jpg.txt', '42.jpg.txt', '43.png.txt', '44.png.txt', '45.png.txt', '46.jpg.txt', '47.png.txt', '48.jpg.txt', '49.jpg.txt', '5.jpg.txt', '50.jpg.txt', '51.jpg.txt', '52.png.txt', '53.png.txt', '54.jpg.txt', '55.png.txt', '56.png.txt', '57.jpg.txt', '58.jpg.txt', '59.png.txt', '6.jpg.txt', '60.jpg.txt', '61.jpg.txt', '62.jpg.txt', '63.jpg.txt', '64.jpg.txt', '65.jpg.txt', '66.jpg.txt', '67.jpg.txt', '68.jpg.txt', '69.jpg.txt', '7.jpg.txt', '70.jpg.txt', '71.jpg.txt', '72.jpg.txt', '73.jpg.txt', '74.jpeg.txt', '75.jpg.txt', '76.jpg.txt', '77.jpg.txt', '78.jpg.txt', '79.jpg.txt', '8.jpg.txt', '80.jpg.txt', '81.png.txt', '82.jpg.txt', '83.jpg.txt', '84.jpg.txt', '85.jpg.txt', '86.gif.txt', '87.jpg.txt', '88.jpg.txt', '89.jpg.txt', '9.jpg.txt', '90.jpg.txt', '91.jpg.txt', '92.png.txt', '93.gif.txt', '94.jpg.txt', '95.jpg.txt', '96.png.txt', '97.jpg.txt', '98.jpg.txt', '99.jpg.txt'};
#Form
form = {'100.jpg.txt', '101.jpg.txt', '102.jpg.txt', '103.png.txt', '104.png.txt', '34.jpg.txt', '35.png.txt', '36.jpg.txt', '37.png.txt', '38.jpg.txt', '38.png.txt', '39.gif.txt', '40.png.txt', '41.jpg.txt', '42.jpg.txt', '43.jpg.txt', '44.jpg.txt', '45.png.txt', '46.png.txt', '47.jpg.txt', '48.jpg.txt', '49.jpg.txt', '50.jpg.txt'};
%formeText
forme2 = {'1.txt', '10.txt', '11.txt', '12.txt', '13.txt', '14.txt', '15.txt', '16.txt', '17.txt', '19.txt', '2.txt', '20.txt', '21.txt', '22.txt', '23.txt', '24.txt', '25.txt', '26.txt', '27.txt', '28.txt', '29.txt', '3.txt', '30.txt', '31.txt', '32.txt', '33.txt', '34.txt', '35.txt', '36.txt', '37.txt', '38.txt', '39.txt', '4.txt', '40.txt', '41.txt', '42.txt', '43.txt', '44.txt', '45.txt', '46.txt', '47.txt', '48.txt', '49.txt', '5.txt', '50.txt', '51.txt', '52.txt', '53.txt', '54.txt', '55.txt', '56.txt', '57.txt', '58.txt', '59.txt', '6.txt', '60.txt', '61.txt', '62.txt', '63.txt', '64.txt', '65.txt', '66.txt', '67.txt', '68.txt', '69.txt', '7.txt', '70.txt', '71.txt', '72.txt', '73.txt', '74.txt', '75.txt', '76.txt', '77.txt', '78.txt', '79.txt', '8.txt', '80.txt', '9.txt'};
%disp(size(id))
disp(size(id1));
disp(size(id2));
disp(size(text));
disp(size(form));
disp(size(forme2));

%form je odradjen
fid = fopen ("data_train.csv", "a");
for i=1:length(text),
  disp(i);
  
  stemmed_text = processText(readFile(text{1,i}),"complete_vocab.txt");
  #stemmed_text = readFile(form{1,i});
  #stemmed_text = readFile(id{1,i});
  
  if(!strcmp(stemmed_text,""))
    fprintf(fid, "%s,%d\n", stemmed_text, 2);
  end
end
fclose(fid);
retVal = 1;
endfunction
