# LoggerPerfTest
Demonstrates a memory leak problem with the use of NLog asynchronously using ${threadid}, GDC or MLDC in the layout.

With the threadid or GDC or MLDC in the layout of a target with async=true, the current thread blocks and the memory increases until a crash (OutOfMemory).

In the code (Program.cs or HomeController.cs) uncomment the 'PerfTestCrash' logger to see the exception.
Work fine with the 'PerfTest' logger.

** WARNING This example generate a >3GB log file. **