# OCR-With-Machine-Learning
Inovatec internship project. 

ASP.NET Web application where users can upload their scanned images (identification documents, forms and textual documents) and perform OCR - Optical Character Recognition to recognize text from uploaded images and classify those images in three mentioned groups.
Used technologies: ASP.NET, MS SQL, Tesseract OCR lib, libsvm, AJAX... 

## How to run project? ##
- Open solution and try to start project (Build it)
- Add references to packages folder
- Delete Emgu.CV.World reference
- Add Emgu.CV.World reference from folder EMGU dll
- Copy dlls from IIS Express dlls folder to your IIS folder.
- Open DatabaseConfig.txt and execute queries.
- Change connection string in web.config file.
- Start project as Administrator.
- That's it.

## Screenshots:

[![Screen Shot](https://i.imgur.com/pJ6l1zT.png)](#)<br>
'Welcome screen' (only front end validation)<br><br>

[![Screen Shot](https://i.imgur.com/EAACMlR.png)](#)<br>
Login screen<br><br>

[![Screen Shot](https://i.imgur.com/v8Ir29k.png)](#)<br>
Main page<br><br>

[![Screen Shot](https://i.imgur.com/QuByqkK.png)](#)<br>
Preview document<br><br>

[![Screen Shot](https://i.imgur.com/XU2G1Lu.png)](#)<br>
Uploaded document<br><br>

[![Screen Shot](https://i.imgur.com/IPW03xk.png)](#)<br>
Opened document - started OCR<br><br>

[![Screen Shot](https://i.imgur.com/IBkkZFw.png)](#)<br>
Opened document - started Classification<br><br>

[![Screen Shot](https://i.imgur.com/f77tJdm.png)](#)<br>
Opened document - Classified document<br><br>

## Licence

Source code can be found on [github](https://github.com/georgeOsdDev/markdown-edit), licenced under [MIT](http://opensource.org/licenses/mit-license.php).

Developed by [Bokunda] & Internship team(#) 
© 2018.
