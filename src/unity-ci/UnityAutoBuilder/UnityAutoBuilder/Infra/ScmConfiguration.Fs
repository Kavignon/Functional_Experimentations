
module UnityAutoBuilder.SourceControlManagerConfiguration
open System
open System.IO

open FsToolkit.ErrorHandling

type SupportedScm =
    | Git
    
type GitRepositoryInformation = {
    projectName: string
    gitRepositoryPath: string
    gitMainBranchName: string
    hostedServerName: string
}

type ScmUserCredentials = {
    username: string
    password: string
}

type LocalGitFolder = {
    directoryPath: string
    subDirectoryPath: string option
}

type CloneCommandArgs = {
    repositoryInformation: GitRepositoryInformation
    userCredentials: ScmUserCredentials
    localGitFolder: LocalGitFolder
}
    
type GitCommand =
    | Clone of CloneCommandArgs
    
type ScmSupportedCommand =
    | GitCommand of GitCommand
    
let validateGitCommand (gitCommand: GitCommand) : Validation<unit, string> =
    let validateUserCredentials userCredentials =
        if String.IsNullOrEmpty(userCredentials.username) then
            Error "Username is null or empty"
        elif String.IsNullOrEmpty(userCredentials.password) then
            Error "Password is null or empty"
        else
            Ok ()
            
    let validateLocalGitFolder gitFolder projectName =
        if String.IsNullOrEmpty(gitFolder.directoryPath) then
            Error "Directory path is null or empty"
        elif not (Directory.Exists(gitFolder.directoryPath)) then
            Error "Directory path does not exist"
        elif (Option.isSome gitFolder.subDirectoryPath) && not (Directory.Exists(Path.Combine(gitFolder.directoryPath, gitFolder.subDirectoryPath.Value))) then
             Error "Directory path does not exist"
        else
            Ok ()
            
    let validateGitRepositoryInformation repositoryInfo =
        if String.IsNullOrEmpty(repositoryInfo.projectName) then
            Error "Project name is null or empty"
        elif String.IsNullOrEmpty(repositoryInfo.gitRepositoryPath) then
            Error "Git repository path is null or empty"
        elif String.IsNullOrEmpty(repositoryInfo.gitMainBranchName) then
            Error "Git main branch name is null or empty"
        elif String.IsNullOrEmpty(repositoryInfo.hostedServerName) then
            Error "Hosted server name is null or empty"
        else
            Ok ()
    
    match gitCommand with
    | Clone args ->
        validation {
            let! _ = validateUserCredentials args.userCredentials
            and! _ = validateLocalGitFolder args.localGitFolder args.repositoryInformation.projectName
            and! _ = validateGitRepositoryInformation args.repositoryInformation
            return ()
        } 
        
let cloneRepository (scmCommand: ScmSupportedCommand) =
    match scmCommand with
    | GitCommand (Clone args) ->
        match validateGitCommand (Clone args) with
        | Error listOfErrors ->
            listOfErrors
            |> List.iter (fun error -> printfn $"Error: %s{error}")
        | Ok () ->
            () // TODO: Implement the local process infrastructure executing SCM commands.