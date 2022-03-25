# LogDLL

To use this dll just add a reference in your Visual Studio project. Project -> Add Reference -> Browse to .DLL -> Click Okay.

Then at the top of your cs file add Using tLogger:   
Using tLogger;

To us in your script:   
var log = new Log();   
var timeStamp = DateTime.Today.ToString("MM-dd-yyyy");    
var logFileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\LOGS\\Log-{timeStamp}.txt";   
log.Write("This is my log message",logFileName);   


reference : https://gist.github.com/tommyready/af3ca0772fdc04b8e9936b90a495ce92
