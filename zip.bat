del BallisticCalculator2.zip
del BallisticCalculator2.src.zip
7z a -r -tzip BallisticCalculator2.zip c:\BallisticCalculator\*.*
7z d -r BallisticCalculator2.zip *.trace *.xml
7z a -r -tzip BallisticCalculator2.src.zip -xr!bin -xr!obj -xr!.svn -x!*.zip