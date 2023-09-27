module UnityAutoBuilder.SourceControlManagerConfiguration

// ** Source Control System Configuration ** //
// As of now, for the experiment, we only support Git.
// We will add support for other source control systems later.
// I'm adding a few todos to keep track of what we need to do.
// They'll be removed once we've implemented the feature.

// TODO: Add support for other source control systems
// TODO: Add support for generic source control system configuration
// TODO: Add support for generic source control system commands
// TODO: Add support for generic source control system commit message template
// TODO: Add support for generic source control system commit message template variables

// Over time, we should move to an open configuration allowing to use any source control system.
// This will require to add a new configuration file for each source control system.
// The configuration file will be loaded dynamically based on the source control system used.
// The configuration file should also specify the commands that we can support for each source control system.
// The configuration file should also allow to specify the variables that can be used in the template.
// The configuration file should also allow to specify the commit message template variables.
// The configuration file could be a JSON file with the following structure:
// {
//     "sourceControlSystem": "Git",
//     "sourceControlSystemConfiguration": {
//         "gitConfiguration": {
//             "gitPath": "C:\\Program Files\\Git\\bin\\git.exe",
//             "gitRepositoryPath": "C:\\Users\\user\\Documents\\GitHub\\UnityAutoBuilder",
//             "gitBranch": "master",
//             "gitCommitMessage": "Build from UnityAutoBuilder"
//         },
//         "gitCommands": {
//             "gitStatus": "git status",
//             "gitAdd": "git add",
//             "gitCommit": "git commit",
//             "gitPush": "git push",
//             "gitTag": "git tag",
//             "gitPushTag": "git push --tags"
//         },
//         "gitCommitMessageTemplate": "Build from UnityAutoBuilder - {0} - {1} - {2}",
//         "gitCommitMessageTemplateVariables": [
//             "BuildNumber",
//             "BuildTarget",
//             "BuildPlatform"
//         ]
//     }
// }

