$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

function Initalize(){
    [cmdletbinding()]
    param()
    process{
        'Loading riddlerps module' | Write-Output
        Import-Module -Name (Join-Path $scriptDir "riddlerps.psm1") -DisableNameChecking -Force
    }    
}

function PromptForProjectType{
    [cmdletbinding()]
    param()
    process{
        $projectPrompt = @(
            New-PromptObject `
            -promptType PickOne `
            -text "`r`nSelect the project" `
                -options ([ordered]@{
                'blazor'='BlazorWebApp01'
                'MVC'='MVCWeb01'
                'RazorPages'='RazorPages01'
                'API Controllers'='WebApiControllers'
                'API Endpoints'='WebApiEndpoints01'
                'quit'='Quit'
            })
        )

        $promptResult = Invoke-Prompts $projectPrompt

        return $promptResult
    }
}

function QuitUnknownParameter{
    [cmdletbinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$unknownParam
    )
    process{
        'Unknown parameter: {0}' -f $unknownParam | Write-Output
        'Exiting script' | Write-Output
        exit
    }
}

function PrintPromptDataForProject{
    [cmdletbinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$projectType
    )
    process{
        switch($projectType){
            'blazor' {PrintDataBlazor}
            'MVC' {PrintDataMVC}
            'RazorPages' {PrintDataRazorPages}
            'API Controllers' {PrintDataApiControllers}
            'API Endpoints' {PrintDataApiEndpoints}
            default          { QuitUnknownParameter -unknownParam $projectType }
        }
    }
}

function PrintDataBlazor{
    [cmdletbinding()]
    param()
    process{
        $projectName = 'BlazorWebApp01'

        $data = @'
# Data:        
- ProjectName: {0}
- ProjectFilePath: {0}/{0}.csproj
- ModelName: Contact
- ModelFilePath: {0}/Models/Contact.cs
- ModelPluralName: Contacts
- DotNetVersion: 9
- DbProvider: SQLite
- EfProviderPackage: Microsoft.EntityFrameworkCore.Sqlite
- DbContextClass: ApplicationDbContext
- OutputPath: {0}/Components/Pages/Contacts
- ScaffoldType: RazorComponent
- MigrationName: InitialCreate
- GenerateCrudOperations: Create, Read, Update, Delete and Details
- RequiresValidation: true
- AddNavigationLink: true
'@ -f $projectName

        $data | Write-Output

    }
}

function PrintDataMVC{
    [cmdletbinding()]
    param()
    process{
        $projectName = 'MVCWeb01'

        $data = @'
# Data:
- ProjectName: {0}
- ProjectFilePath: {0}/{0}.csproj
- ModelName: Contact
- ModelFilePath: {0}/Models/Contact.cs
- ModelPluralName: Contacts
- DotNetVersion: 9
- DbProvider: SQLite
- EfProviderPackage: Microsoft.EntityFrameworkCore.Sqlite
- DbContextClass: ApplicationDbContext
- OutputPath: {0}/Controllers/Contacts
- ScaffoldType: Controller
- MigrationName: InitialCreate
- GenerateCrudOperations: Create, Read, Update, Delete and Details
- RequiresValidation: true
- AddNavigationLink: true

'@ -f $projectName

        $data | Write-Output
    }
}

function PrintDataRazorPages{
    [cmdletbinding()]
    param()
    process{
        $projectName = 'RazorPages01'

        $data = @'
# Data:
- ProjectName: {0}
- ProjectFilePath: {0}/{0}.csproj
- ModelName: Contact
- ModelFilePath: {0}/Models/Contact.cs
- ModelPluralName: Contacts
- DotNetVersion: 9
- DbProvider: SQLite
- EfProviderPackage: Microsoft.EntityFrameworkCore.Sqlite
- DbContextClass: ApplicationDbContext
- OutputPath: {0}/Pages/Contacts
- ScaffoldType: RazorPages
- MigrationName: InitialCreate
- GenerateCrudOperations: Create, Read, Update, Delete and Details
- RequiresValidation: true
- AddNavigationLink: true

'@ -f $projectName

        $data | Write-Output
    }
}

function PrintDataApiControllers{
    [cmdletbinding()]
    param()
    process{
$projectName = 'WebApiControllers'

        $data = @'
# Data:
- ProjectName: {0}
- ProjectFilePath: {0}/{0}.csproj
- ModelName: Contact
- ModelFilePath: {0}/Models/Contact.cs
- ModelPluralName: Contacts
- DotNetVersion: 9
- DbProvider: SQLite
- EfProviderPackage: Microsoft.EntityFrameworkCore.Sqlite
- DbContextClass: ApplicationDbContext
- OutputPath: {0}/Controllers/Contacts
- ScaffoldType: Controller
- MigrationName: InitialCreate
- GenerateCrudOperations: Create, Read, Update, Delete and Details
- RequiresValidation: true
- AddNavigationLink: false

'@ -f $projectName

        $data | Write-Output
    }
}

function PrintDataApiEndpoints{
    [cmdletbinding()]
    param()
    process{
        $projectName = 'WebApiEndpoints01'

        $data = @'
# Data:
- ProjectName: {0}
- ProjectFilePath: {0}/{0}.csproj
- ModelName: Contact
- ModelFilePath: {0}/Models/Contact.cs
- ModelPluralName: Contacts
- DotNetVersion: 9
- DbProvider: SQLite
- EfProviderPackage: Microsoft.EntityFrameworkCore.Sqlite
- DbContextClass: ApplicationDbContext
- OutputPath: {0}/Api/Contacts
- ScaffoldType: ApiEndpoints
- ApiEndpointFilePath: WebApiEndpoints01/Api/ContactEndpoints.cs
- MigrationName: InitialCreate
- GenerateCrudOperations: Create, Read, Update, Delete and Details
- RequiresValidation: true
- AddNavigationLink: false

'@ -f $projectName

        $data | Write-Output
    }
}

function CheckForQuit{
    [cmdletbinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$promptResult
    )
    process{
        if($promptResult -eq 'quit'){
            'Exiting script' | Write-Output
            exit
        }
    }
}

# begin script
Initalize

$projectType = (PromptForProjectType)['userprompt']

'Project type: "{0}"' -f $projectType | Write-Output
"" | Write-Output
CheckForQuit -promptResult $projectType

PrintPromptDataForProject -projectType $projectType





