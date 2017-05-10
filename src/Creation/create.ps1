$code = {

    $data_small = "C:\Temp\zoomData\4c77b524-1857-4550-afaa-c0ebe5e3960a_101013-0000.mp3.small"
    $data_large = "C:\Temp\zoomData\4c77b524-1857-4550-afaa-c0ebe5e3960a_101013-0000.mp3"

    function dest($name) {
        "C:\Temp\zoomData\$name"
    }

    $sql_lite_binary = "C:\Work\Github\index-file-perf-test\bin\sqlite-tools-win32-x86-3170000\sqlite3.exe"
    function Create-SqlLite($source, $dest, $row_id, $page_size) {
        $pragma = "PRAGMA page_size = $page_size;"
        $create =  "CREATE TABLE file_list (filename TEXT PRIMARY KEY, blob BLOB NOT NULL)"
        if ($row_id) {
            $create += ";"
        }
        else {
            $create += " WITHOUT ROWID;"
        }

        . $sql_lite_binary $dest ($pragma + $create)

        $files = ls $source
        foreach ($file in $files) {
            $insert = "INSERT INTO file_list VALUES('$($file.Name)', readfile('$($file.FullName)'))"
            . $sql_lite_binary $dest $insert
        }

        . $sql_lite_binary $dest "VACUUM;"
    }

    function Create-Archive($source, $dest, $args) {
        7za a $args $dest $source
    }

    # import PSH5X
    # Note: The value for PowerShellHostVersion in HDF5.psd1 is incorrect. Patch it by setting to ''.
    Import-Module "C:\Users\Anthony\OneDrive\Specials\WindowsPowershell\Modules\HDF5"
    function Create-HDF5($source, $dest) {
        # create the file
        New-H5File $dest
        $h5_file = Get-Item $dest

        # mount the powershell drive
        $drive_name = $h5_file.Name -replace "[\[\]./\\:]","_"
        New-H5Drive $drive_name ($h5_file.FullName) -RW
        
        # cd into h5 file (drive)
        Push-Location
        cd ($drive_name + ":")

        # for each file
        $files = ls $source
        foreach ($file in $files) {
            # read the bytes
            [byte[]] $fileBytes = [System.IO.File]::ReadAllBytes($file.FullName)

            # create the dataset
            New-H5Dataset ($file.Name) "opaque$($fileBytes.Count)" -Scalar
        
            # set the value
            Set-H5DatasetValue ($file.Name) (,$fileBytes)
        }

        # finally unmount the drive (and close off HDF5 file)
        Pop-Location
        Remove-H5Drive $drive_name
    }
}


# tar & zip
start-job { Create-Archive ($data_small + "\*")  (dest "small.tar") "-ttar"                  } -init $code
start-job { Create-Archive ($data_large + "\*")  (dest "large.tar") "-ttar"                  } -init $code
start-job { Create-Archive ($data_small + "\*")  (dest "small.zip") "-tzip -mx0"             } -init $code
start-job { Create-Archive ($data_large + "\*")  (dest "large.zip") "-tzip -mx0"             } -init $code

# sqllite 
start-job { Create-SqlLite $data_small (dest "small.rowid.8192.sqlite3") $true 8192           } -init $code
start-job { Create-SqlLite $data_small (dest "small.wo_rowid.8192.sqlite3") $false 8192       } -init $code
start-job { Create-SqlLite $data_small (dest "small.rowid.16384.sqlite3") $true 16384         } -init $code
start-job { Create-SqlLite $data_small (dest "small.wo_rowid.16384.sqlite3") $false 16384     } -init $code
start-job { Create-SqlLite $data_small (dest "small.rowid.32768.sqlite3") $true 32768         } -init $code
start-job { Create-SqlLite $data_small (dest "small.wo_rowid.32768.sqlite3") $false 32768     } -init $code
start-job { Create-SqlLite $data_large (dest "large.rowid.8192.sqlite3") $true 8192           } -init $code
start-job { Create-SqlLite $data_large (dest "large.wo_rowid.8192.sqlite3") $false 8192       } -init $code
start-job { Create-SqlLite $data_large (dest "large.rowid.16384.sqlite3") $true 16384         } -init $code
start-job { Create-SqlLite $data_large (dest "large.wo_rowid.16384.sqlite3") $false 16384     } -init $code
start-job { Create-SqlLite $data_large (dest "large.rowid.32768.sqlite3") $true 32768         } -init $code
start-job { Create-SqlLite $data_large (dest "large.wo_rowid.32768.sqlite3") $false 32768     } -init $code

# hdf5
start-job { Create-HDF5 $data_small (dest "small.h5")     } -init $code
start-job { Create-HDF5 $data_large (dest "large.h5")     } -init $code
