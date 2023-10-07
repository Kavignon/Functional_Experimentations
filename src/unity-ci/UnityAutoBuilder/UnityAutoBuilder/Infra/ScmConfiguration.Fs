
module UnityAutoBuilder.SourceControlManagerConfiguration

open System
open System.IO

open UnityAutoBuilder.Utilities.WrappedString

type SupportedScm =
    | Git
    
type WebHostServer =
    | GitHub
    | BitBucket
    | AzureDevOps
    | GitLab
    
type OnlineRepositoryMetadata = {
    sourceControlManager: SupportedScm
    hostedServer: WebHostServer
    hostedServerProjectUrl: Uri
    hostedServerProjectName: String100
}
with
    static member create 
        (sourceControlManager: string)
        (hostedServer: string)
        (hostedServerProjectUrl: string)
        (hostedServerProjectName: string) =
        let tryHostedServerProjectName = tryBuildStr100 hostedServerProjectName
        let tryHostedServerProjectUrl = Uri.TryCreate(hostedServerProjectUrl, UriKind.Absolute)
        
        match tryHostedServerProjectName, tryHostedServerProjectUrl with
        | Some hostedServerProjectName, (true, hostedServerProjectUrl) ->
            Some {
                sourceControlManager = sourceControlManager
                hostedServer = hostedServer
                hostedServerProjectUrl = hostedServerProjectUrl
                hostedServerProjectName = hostedServerProjectName
            }
        | _ ->
            None

type LocalRepositoryMetadata = {
    rootPath: DirectoryInfo
    mainBranchName: String100
}
with
    static member create
        (rootPath: string)
        (mainBranchName: string) =
        let tryRootPath = tryBuildStr100 rootPath
        let tryMainBranchName = tryBuildStr100 mainBranchName
        
        match tryRootPath, tryMainBranchName with
        | Some rootPath, Some mainBranchName ->
            Some {
                rootPath = DirectoryInfo(unwrap rootPath)
                mainBranchName = mainBranchName
            }
        | _ ->
            None

type CloneCommandArgument = {
    onlineRepository: OnlineRepositoryMetadata
    localRepository: LocalRepositoryMetadata
}
        
type GitCommand =
    | Clone of CloneCommandArgument
    
type ScmSupportedCommand =
    | GitCommand of GitCommand