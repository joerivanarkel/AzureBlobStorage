$resourcegroup = 'fillinresourcegroupname'
$storagename = 'fillinstoragename'

New-AzResourceGroup -Name $resourcegroup -Location northeurope -Force

New-AzResourceGroupDeployment `
    -Name 'new-storage' `
    -ResourceGroupName $resourcegroup `
    -TemplateFile 'StorageAccountTemplate.json' `
    -StorageName $storagename