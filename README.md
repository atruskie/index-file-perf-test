# Index-File-Perf-Test



# Downloading tools

- SQLLite
    - 3.17.0
    - Windows: https://sqlite.org/2017/sqlite-tools-win32-x86-3170000.zip
    - Unix: https://sqlite.org/2017/sqlite-tools-linux-x86-3170000.zip
- Tar
    - Version: 1.29
    - Windows: packaged with Git installs
    - Unix: native
- HDF5
    - Version 1.10
    - https://support.hdfgroup.org/HDF5/release/obtain5110.html
    - I'm using the old HDF5DotNet project (rather than the more modern PInvoke one)
        - It requires the !old! HDF5 libs to be installed on target computer!
        - And the libs must be on PATH
        - http://hdf5.net/downloads/amd64/HDF5DotNet-Net4.0-x86_64.zip
    - Windows: https://github.com/HDFGroup/psh5x
- unzip (Info-Zip)
    - Windows
        - Version 6.0
        - choco install unzip
    - Unix
        - Version 6.0
        - sudo apt-get install zip

Requires the following binaries on PATH (both Windows and Unix):

- cp
- ls
- RScript[.exe]


# Creating files

See `create.ps1`

# Running the tests

- Open solution in Visual Studio 2017
- Restore packages with Nuget
- Build the solution in `RELEASE`
- From base directory, execute `[mono] ./src/IndexFilePerfTest/bin/Release/IndexFilePerfTest.exe`

# Related work

- Moving away from HDF5 http://cyrille.rossant.net/moving-away-hdf5/
- SQLite As An Application File Format https://www.sqlite.org/appfileformat.html

# Format Notes

- Raw
    - Patently unusable at large scales
    - Only option that supports parallel writes
    - Block size on disk massively affects overhead
- SQLite
    - Does not seem to have a standard file ext, but we'll use `.sqlite3`
    - NFS File Locks are known to be buggy - best to write locally then xfer. 
    - Parallel writes through synchronous writer queue (custom or lib?).
    - Need to Vacuum after creation
    - Memory mapped files might be a very fast way to create these files (various caveats involved)
    - we should use COLLATE BINARY for `filename` column
    - schema:
        - (PK) filename TEXT
        - blob BLOB
    - We don't need ROWID functionality (https://www.sqlite.org/lang_createtable.html#rowid)
        - https://www.sqlite.org/withoutrowid.html
    - There a performance tradeoffs for different size page files (https://www.sqlite.org/intern-v-extern-blob.html)
    - The `writefile` function only exists in the CLI shell!
    - reference commands:
    ```
    PRAGMA page_size = 16384
    ```
    ```
    CREATE TABLE file_list (
      filename TEXT PRIMARY KEY,
      blob BLOB NOT NULL,
     table_constraint
    ) WITHOUT ROWID;
    ```
    ```
    INSERT INTO table(file_list) VALUES('some_file.ext', readfile('some_path/some_file.ext'))
    ```
    ```
    SELECT writefile('some_file.png',blob) FROM file_list WHERE filename='icon';
    ```
- HDF5
    - There are many binaries 
    - Standard extension seems to be `.h5`
    - dataset structure: https://support.hdfgroup.org/HDF5/doc1.6/UG/10_Datasets.html
    - dataset types: https://support.hdfgroup.org/HDF5/doc1.6/UG/11_Datatypes.html
    - there are bloody powershell provider bindings!
        - https://support.hdfgroup.org/projects/PSH5X/
        - https://support.hdfgroup.org/projects/PSH5X/cmdlets.html
        - Import-Module "C:\Users\Anthony\OneDrive\Specials\WindowsPowershell\Modules\HDF5"
        - New-H5File ./small.h5
        - New-H5Drive demo .\small.h5 -RW
        - New-H5Dataset 'some_file.ext' -Type "Opaque32" 1000
    - CLI tools:
        - h5dump.exe -d /some_file.ext -b FILE -o some_file.ext .\small.h5
        - h5ls.exe .\small.h5
    - Sample command:
        - h5import my_imagesome_file.ext -c config_file -o out_file