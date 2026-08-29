namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The root query for implementing GraphQL mutations.
    /// </summary>
    public class Mutation : QueryableValue<Mutation>, IMutation
    {
        public Mutation() : base(null)
        {
        }

        internal Mutation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Clear all of a customer's queued migrations
        /// </summary>
        /// <param name="input">Parameters for AbortQueuedMigrations</param>
        public AbortQueuedMigrationsPayload AbortQueuedMigrations(Arg<AbortQueuedMigrationsInput> input) => this.CreateMethodCall(x => x.AbortQueuedMigrations(input), Octokit.GraphQL.Model.AbortQueuedMigrationsPayload.Create);

        /// <summary>
        /// Abort a repository migration queued or in progress.
        /// </summary>
        /// <param name="input">Parameters for AbortRepositoryMigration</param>
        public AbortRepositoryMigrationPayload AbortRepositoryMigration(Arg<AbortRepositoryMigrationInput> input) => this.CreateMethodCall(x => x.AbortRepositoryMigration(input), Octokit.GraphQL.Model.AbortRepositoryMigrationPayload.Create);

        /// <summary>
        /// Accepts a pending invitation for a user to become an administrator of an enterprise.
        /// </summary>
        /// <param name="input">Parameters for AcceptEnterpriseAdministratorInvitation</param>
        public AcceptEnterpriseAdministratorInvitationPayload AcceptEnterpriseAdministratorInvitation(Arg<AcceptEnterpriseAdministratorInvitationInput> input) => this.CreateMethodCall(x => x.AcceptEnterpriseAdministratorInvitation(input), Octokit.GraphQL.Model.AcceptEnterpriseAdministratorInvitationPayload.Create);

        /// <summary>
        /// Applies a suggested topic to the repository.
        /// </summary>
        /// <param name="input">Parameters for AcceptTopicSuggestion</param>
        public AcceptTopicSuggestionPayload AcceptTopicSuggestion(Arg<AcceptTopicSuggestionInput> input) => this.CreateMethodCall(x => x.AcceptTopicSuggestion(input), Octokit.GraphQL.Model.AcceptTopicSuggestionPayload.Create);

        /// <summary>
        /// Adds assignees to an assignable object.
        /// </summary>
        /// <param name="input">Parameters for AddAssigneesToAssignable</param>
        public AddAssigneesToAssignablePayload AddAssigneesToAssignable(Arg<AddAssigneesToAssignableInput> input) => this.CreateMethodCall(x => x.AddAssigneesToAssignable(input), Octokit.GraphQL.Model.AddAssigneesToAssignablePayload.Create);

        /// <summary>
        /// Adds a comment to an Issue or Pull Request.
        /// </summary>
        /// <param name="input">Parameters for AddComment</param>
        public AddCommentPayload AddComment(Arg<AddCommentInput> input) => this.CreateMethodCall(x => x.AddComment(input), Octokit.GraphQL.Model.AddCommentPayload.Create);

        /// <summary>
        /// Adds a comment to a Discussion, possibly as a reply to another comment.
        /// </summary>
        /// <param name="input">Parameters for AddDiscussionComment</param>
        public AddDiscussionCommentPayload AddDiscussionComment(Arg<AddDiscussionCommentInput> input) => this.CreateMethodCall(x => x.AddDiscussionComment(input), Octokit.GraphQL.Model.AddDiscussionCommentPayload.Create);

        /// <summary>
        /// Vote for an option in a discussion poll.
        /// </summary>
        /// <param name="input">Parameters for AddDiscussionPollVote</param>
        public AddDiscussionPollVotePayload AddDiscussionPollVote(Arg<AddDiscussionPollVoteInput> input) => this.CreateMethodCall(x => x.AddDiscussionPollVote(input), Octokit.GraphQL.Model.AddDiscussionPollVotePayload.Create);

        /// <summary>
        /// Adds enterprise members to an organization within the enterprise.
        /// </summary>
        /// <param name="input">Parameters for AddEnterpriseOrganizationMember</param>
        public AddEnterpriseOrganizationMemberPayload AddEnterpriseOrganizationMember(Arg<AddEnterpriseOrganizationMemberInput> input) => this.CreateMethodCall(x => x.AddEnterpriseOrganizationMember(input), Octokit.GraphQL.Model.AddEnterpriseOrganizationMemberPayload.Create);

        /// <summary>
        /// Adds a support entitlement to an enterprise member.
        /// </summary>
        /// <param name="input">Parameters for AddEnterpriseSupportEntitlement</param>
        public AddEnterpriseSupportEntitlementPayload AddEnterpriseSupportEntitlement(Arg<AddEnterpriseSupportEntitlementInput> input) => this.CreateMethodCall(x => x.AddEnterpriseSupportEntitlement(input), Octokit.GraphQL.Model.AddEnterpriseSupportEntitlementPayload.Create);

        /// <summary>
        /// Adds labels to a labelable object.
        /// </summary>
        /// <param name="input">Parameters for AddLabelsToLabelable</param>
        public AddLabelsToLabelablePayload AddLabelsToLabelable(Arg<AddLabelsToLabelableInput> input) => this.CreateMethodCall(x => x.AddLabelsToLabelable(input), Octokit.GraphQL.Model.AddLabelsToLabelablePayload.Create);

        /// <summary>
        /// Adds a card to a ProjectColumn. Either `contentId` or `note` must be provided but **not** both.
        /// </summary>
        /// <param name="input">Parameters for AddProjectCard</param>
        public AddProjectCardPayload AddProjectCard(Arg<AddProjectCardInput> input) => this.CreateMethodCall(x => x.AddProjectCard(input), Octokit.GraphQL.Model.AddProjectCardPayload.Create);

        /// <summary>
        /// Adds a column to a Project.
        /// </summary>
        /// <param name="input">Parameters for AddProjectColumn</param>
        public AddProjectColumnPayload AddProjectColumn(Arg<AddProjectColumnInput> input) => this.CreateMethodCall(x => x.AddProjectColumn(input), Octokit.GraphQL.Model.AddProjectColumnPayload.Create);

        /// <summary>
        /// Creates a new draft issue and add it to a Project.
        /// </summary>
        /// <param name="input">Parameters for AddProjectV2DraftIssue</param>
        public AddProjectV2DraftIssuePayload AddProjectV2DraftIssue(Arg<AddProjectV2DraftIssueInput> input) => this.CreateMethodCall(x => x.AddProjectV2DraftIssue(input), Octokit.GraphQL.Model.AddProjectV2DraftIssuePayload.Create);

        /// <summary>
        /// Links an existing content instance to a Project.
        /// </summary>
        /// <param name="input">Parameters for AddProjectV2ItemById</param>
        public AddProjectV2ItemByIdPayload AddProjectV2ItemById(Arg<AddProjectV2ItemByIdInput> input) => this.CreateMethodCall(x => x.AddProjectV2ItemById(input), Octokit.GraphQL.Model.AddProjectV2ItemByIdPayload.Create);

        /// <summary>
        /// Adds a review to a Pull Request.
        /// </summary>
        /// <param name="input">Parameters for AddPullRequestReview</param>
        public AddPullRequestReviewPayload AddPullRequestReview(Arg<AddPullRequestReviewInput> input) => this.CreateMethodCall(x => x.AddPullRequestReview(input), Octokit.GraphQL.Model.AddPullRequestReviewPayload.Create);

        /// <summary>
        /// Adds a comment to a review.
        /// </summary>
        /// <param name="input">Parameters for AddPullRequestReviewComment</param>
        public AddPullRequestReviewCommentPayload AddPullRequestReviewComment(Arg<AddPullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.AddPullRequestReviewComment(input), Octokit.GraphQL.Model.AddPullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Adds a new thread to a pending Pull Request Review.
        /// </summary>
        /// <param name="input">Parameters for AddPullRequestReviewThread</param>
        public AddPullRequestReviewThreadPayload AddPullRequestReviewThread(Arg<AddPullRequestReviewThreadInput> input) => this.CreateMethodCall(x => x.AddPullRequestReviewThread(input), Octokit.GraphQL.Model.AddPullRequestReviewThreadPayload.Create);

        /// <summary>
        /// Adds a reply to an existing Pull Request Review Thread.
        /// </summary>
        /// <param name="input">Parameters for AddPullRequestReviewThreadReply</param>
        public AddPullRequestReviewThreadReplyPayload AddPullRequestReviewThreadReply(Arg<AddPullRequestReviewThreadReplyInput> input) => this.CreateMethodCall(x => x.AddPullRequestReviewThreadReply(input), Octokit.GraphQL.Model.AddPullRequestReviewThreadReplyPayload.Create);

        /// <summary>
        /// Adds a reaction to a subject.
        /// </summary>
        /// <param name="input">Parameters for AddReaction</param>
        public AddReactionPayload AddReaction(Arg<AddReactionInput> input) => this.CreateMethodCall(x => x.AddReaction(input), Octokit.GraphQL.Model.AddReactionPayload.Create);

        /// <summary>
        /// Adds a star to a Starrable.
        /// </summary>
        /// <param name="input">Parameters for AddStar</param>
        public AddStarPayload AddStar(Arg<AddStarInput> input) => this.CreateMethodCall(x => x.AddStar(input), Octokit.GraphQL.Model.AddStarPayload.Create);

        /// <summary>
        /// Add an upvote to a discussion or discussion comment.
        /// </summary>
        /// <param name="input">Parameters for AddUpvote</param>
        public AddUpvotePayload AddUpvote(Arg<AddUpvoteInput> input) => this.CreateMethodCall(x => x.AddUpvote(input), Octokit.GraphQL.Model.AddUpvotePayload.Create);

        /// <summary>
        /// Adds a verifiable domain to an owning account.
        /// </summary>
        /// <param name="input">Parameters for AddVerifiableDomain</param>
        public AddVerifiableDomainPayload AddVerifiableDomain(Arg<AddVerifiableDomainInput> input) => this.CreateMethodCall(x => x.AddVerifiableDomain(input), Octokit.GraphQL.Model.AddVerifiableDomainPayload.Create);

        /// <summary>
        /// Approve all pending deployments under one or more environments
        /// </summary>
        /// <param name="input">Parameters for ApproveDeployments</param>
        public ApproveDeploymentsPayload ApproveDeployments(Arg<ApproveDeploymentsInput> input) => this.CreateMethodCall(x => x.ApproveDeployments(input), Octokit.GraphQL.Model.ApproveDeploymentsPayload.Create);

        /// <summary>
        /// Approve a verifiable domain for notification delivery.
        /// </summary>
        /// <param name="input">Parameters for ApproveVerifiableDomain</param>
        public ApproveVerifiableDomainPayload ApproveVerifiableDomain(Arg<ApproveVerifiableDomainInput> input) => this.CreateMethodCall(x => x.ApproveVerifiableDomain(input), Octokit.GraphQL.Model.ApproveVerifiableDomainPayload.Create);

        /// <summary>
        /// Archives a ProjectV2Item
        /// </summary>
        /// <param name="input">Parameters for ArchiveProjectV2Item</param>
        public ArchiveProjectV2ItemPayload ArchiveProjectV2Item(Arg<ArchiveProjectV2ItemInput> input) => this.CreateMethodCall(x => x.ArchiveProjectV2Item(input), Octokit.GraphQL.Model.ArchiveProjectV2ItemPayload.Create);

        /// <summary>
        /// Marks a repository as archived.
        /// </summary>
        /// <param name="input">Parameters for ArchiveRepository</param>
        public ArchiveRepositoryPayload ArchiveRepository(Arg<ArchiveRepositoryInput> input) => this.CreateMethodCall(x => x.ArchiveRepository(input), Octokit.GraphQL.Model.ArchiveRepositoryPayload.Create);

        /// <summary>
        /// Cancels a pending invitation for an administrator to join an enterprise.
        /// </summary>
        /// <param name="input">Parameters for CancelEnterpriseAdminInvitation</param>
        public CancelEnterpriseAdminInvitationPayload CancelEnterpriseAdminInvitation(Arg<CancelEnterpriseAdminInvitationInput> input) => this.CreateMethodCall(x => x.CancelEnterpriseAdminInvitation(input), Octokit.GraphQL.Model.CancelEnterpriseAdminInvitationPayload.Create);

        /// <summary>
        /// Cancel an active sponsorship.
        /// </summary>
        /// <param name="input">Parameters for CancelSponsorship</param>
        public CancelSponsorshipPayload CancelSponsorship(Arg<CancelSponsorshipInput> input) => this.CreateMethodCall(x => x.CancelSponsorship(input), Octokit.GraphQL.Model.CancelSponsorshipPayload.Create);

        /// <summary>
        /// Update your status on GitHub.
        /// </summary>
        /// <param name="input">Parameters for ChangeUserStatus</param>
        public ChangeUserStatusPayload ChangeUserStatus(Arg<ChangeUserStatusInput> input) => this.CreateMethodCall(x => x.ChangeUserStatus(input), Octokit.GraphQL.Model.ChangeUserStatusPayload.Create);

        /// <summary>
        /// Clears all labels from a labelable object.
        /// </summary>
        /// <param name="input">Parameters for ClearLabelsFromLabelable</param>
        public ClearLabelsFromLabelablePayload ClearLabelsFromLabelable(Arg<ClearLabelsFromLabelableInput> input) => this.CreateMethodCall(x => x.ClearLabelsFromLabelable(input), Octokit.GraphQL.Model.ClearLabelsFromLabelablePayload.Create);

        /// <summary>
        /// This mutation clears the value of a field for an item in a Project. Currently only text, number, date, assignees, labels, single-select, iteration and milestone fields are supported.
        /// </summary>
        /// <param name="input">Parameters for ClearProjectV2ItemFieldValue</param>
        public ClearProjectV2ItemFieldValuePayload ClearProjectV2ItemFieldValue(Arg<ClearProjectV2ItemFieldValueInput> input) => this.CreateMethodCall(x => x.ClearProjectV2ItemFieldValue(input), Octokit.GraphQL.Model.ClearProjectV2ItemFieldValuePayload.Create);

        /// <summary>
        /// Creates a new project by cloning configuration from an existing project.
        /// </summary>
        /// <param name="input">Parameters for CloneProject</param>
        public CloneProjectPayload CloneProject(Arg<CloneProjectInput> input) => this.CreateMethodCall(x => x.CloneProject(input), Octokit.GraphQL.Model.CloneProjectPayload.Create);

        /// <summary>
        /// Create a new repository with the same files and directory structure as a template repository.
        /// </summary>
        /// <param name="input">Parameters for CloneTemplateRepository</param>
        public CloneTemplateRepositoryPayload CloneTemplateRepository(Arg<CloneTemplateRepositoryInput> input) => this.CreateMethodCall(x => x.CloneTemplateRepository(input), Octokit.GraphQL.Model.CloneTemplateRepositoryPayload.Create);

        /// <summary>
        /// Close a discussion.
        /// </summary>
        /// <param name="input">Parameters for CloseDiscussion</param>
        public CloseDiscussionPayload CloseDiscussion(Arg<CloseDiscussionInput> input) => this.CreateMethodCall(x => x.CloseDiscussion(input), Octokit.GraphQL.Model.CloseDiscussionPayload.Create);

        /// <summary>
        /// Close an issue.
        /// </summary>
        /// <param name="input">Parameters for CloseIssue</param>
        public CloseIssuePayload CloseIssue(Arg<CloseIssueInput> input) => this.CreateMethodCall(x => x.CloseIssue(input), Octokit.GraphQL.Model.CloseIssuePayload.Create);

        /// <summary>
        /// Close a pull request.
        /// </summary>
        /// <param name="input">Parameters for ClosePullRequest</param>
        public ClosePullRequestPayload ClosePullRequest(Arg<ClosePullRequestInput> input) => this.CreateMethodCall(x => x.ClosePullRequest(input), Octokit.GraphQL.Model.ClosePullRequestPayload.Create);

        /// <summary>
        /// Convert a project note card to one associated with a newly created issue.
        /// </summary>
        /// <param name="input">Parameters for ConvertProjectCardNoteToIssue</param>
        public ConvertProjectCardNoteToIssuePayload ConvertProjectCardNoteToIssue(Arg<ConvertProjectCardNoteToIssueInput> input) => this.CreateMethodCall(x => x.ConvertProjectCardNoteToIssue(input), Octokit.GraphQL.Model.ConvertProjectCardNoteToIssuePayload.Create);

        /// <summary>
        /// Converts a pull request to draft
        /// </summary>
        /// <param name="input">Parameters for ConvertPullRequestToDraft</param>
        public ConvertPullRequestToDraftPayload ConvertPullRequestToDraft(Arg<ConvertPullRequestToDraftInput> input) => this.CreateMethodCall(x => x.ConvertPullRequestToDraft(input), Octokit.GraphQL.Model.ConvertPullRequestToDraftPayload.Create);

        /// <summary>
        /// Copy a project.
        /// </summary>
        /// <param name="input">Parameters for CopyProjectV2</param>
        public CopyProjectV2Payload CopyProjectV2(Arg<CopyProjectV2Input> input) => this.CreateMethodCall(x => x.CopyProjectV2(input), Octokit.GraphQL.Model.CopyProjectV2Payload.Create);

        /// <summary>
        /// Invites a user to claim reattributable data
        /// </summary>
        /// <param name="input">Parameters for CreateAttributionInvitation</param>
        public CreateAttributionInvitationPayload CreateAttributionInvitation(Arg<CreateAttributionInvitationInput> input) => this.CreateMethodCall(x => x.CreateAttributionInvitation(input), Octokit.GraphQL.Model.CreateAttributionInvitationPayload.Create);

        /// <summary>
        /// Create a new branch protection rule
        /// </summary>
        /// <param name="input">Parameters for CreateBranchProtectionRule</param>
        public CreateBranchProtectionRulePayload CreateBranchProtectionRule(Arg<CreateBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.CreateBranchProtectionRule(input), Octokit.GraphQL.Model.CreateBranchProtectionRulePayload.Create);

        /// <summary>
        /// Create a check run.
        /// </summary>
        /// <param name="input">Parameters for CreateCheckRun</param>
        public CreateCheckRunPayload CreateCheckRun(Arg<CreateCheckRunInput> input) => this.CreateMethodCall(x => x.CreateCheckRun(input), Octokit.GraphQL.Model.CreateCheckRunPayload.Create);

        /// <summary>
        /// Create a check suite
        /// </summary>
        /// <param name="input">Parameters for CreateCheckSuite</param>
        public CreateCheckSuitePayload CreateCheckSuite(Arg<CreateCheckSuiteInput> input) => this.CreateMethodCall(x => x.CreateCheckSuite(input), Octokit.GraphQL.Model.CreateCheckSuitePayload.Create);

        /// <summary>
        /// Appends a commit to the given branch as the authenticated user.
        /// This mutation creates a commit whose parent is the HEAD of the provided
        /// branch and also updates that branch to point to the new commit.
        /// It can be thought of as similar to `git commit`.
        /// ### Locating a Branch
        /// Commits are appended to a `branch` of type `Ref`.
        /// This must refer to a git branch (i.e.  the fully qualified path must
        /// begin with `refs/heads/`, although including this prefix is optional.
        /// Callers may specify the `branch` to commit to either by its global node
        /// ID or by passing both of `repositoryNameWithOwner` and `refName`.  For
        /// more details see the documentation for `CommittableBranch`.
        /// ### Describing Changes
        /// `fileChanges` are specified as a `FilesChanges` object describing
        /// `FileAdditions` and `FileDeletions`.
        /// Please see the documentation for `FileChanges` for more information on
        /// how to use this argument to describe any set of file changes.
        /// ### Authorship
        /// Similar to the web commit interface, this mutation does not support
        /// specifying the author or committer of the commit and will not add
        /// support for this in the future.
        /// A commit created by a successful execution of this mutation will be
        /// authored by the owner of the credential which authenticates the API
        /// request.  The committer will be identical to that of commits authored
        /// using the web interface.
        /// If you need full control over author and committer information, please
        /// use the Git Database REST API instead.
        /// ### Commit Signing
        /// Commits made using this mutation are automatically signed by GitHub if
        /// supported and will be marked as verified in the user interface.
        /// </summary>
        /// <param name="input">Parameters for CreateCommitOnBranch</param>
        public CreateCommitOnBranchPayload CreateCommitOnBranch(Arg<CreateCommitOnBranchInput> input) => this.CreateMethodCall(x => x.CreateCommitOnBranch(input), Octokit.GraphQL.Model.CreateCommitOnBranchPayload.Create);

        /// <summary>
        /// Create a discussion.
        /// </summary>
        /// <param name="input">Parameters for CreateDiscussion</param>
        public CreateDiscussionPayload CreateDiscussion(Arg<CreateDiscussionInput> input) => this.CreateMethodCall(x => x.CreateDiscussion(input), Octokit.GraphQL.Model.CreateDiscussionPayload.Create);

        /// <summary>
        /// Creates an organization as part of an enterprise account. A personal access token used to create an organization is implicitly permitted to update the organization it created, if the organization is part of an enterprise that has SAML enabled or uses Enterprise Managed Users. If the organization is not part of such an enterprise, and instead has SAML enabled for it individually, the token will then require SAML authorization to continue working against that organization.
        /// </summary>
        /// <param name="input">Parameters for CreateEnterpriseOrganization</param>
        public CreateEnterpriseOrganizationPayload CreateEnterpriseOrganization(Arg<CreateEnterpriseOrganizationInput> input) => this.CreateMethodCall(x => x.CreateEnterpriseOrganization(input), Octokit.GraphQL.Model.CreateEnterpriseOrganizationPayload.Create);

        /// <summary>
        /// Creates an environment or simply returns it if already exists.
        /// </summary>
        /// <param name="input">Parameters for CreateEnvironment</param>
        public CreateEnvironmentPayload CreateEnvironment(Arg<CreateEnvironmentInput> input) => this.CreateMethodCall(x => x.CreateEnvironment(input), Octokit.GraphQL.Model.CreateEnvironmentPayload.Create);

        /// <summary>
        /// Creates a new IP allow list entry.
        /// </summary>
        /// <param name="input">Parameters for CreateIpAllowListEntry</param>
        public CreateIpAllowListEntryPayload CreateIpAllowListEntry(Arg<CreateIpAllowListEntryInput> input) => this.CreateMethodCall(x => x.CreateIpAllowListEntry(input), Octokit.GraphQL.Model.CreateIpAllowListEntryPayload.Create);

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        /// <param name="input">Parameters for CreateIssue</param>
        public CreateIssuePayload CreateIssue(Arg<CreateIssueInput> input) => this.CreateMethodCall(x => x.CreateIssue(input), Octokit.GraphQL.Model.CreateIssuePayload.Create);

        /// <summary>
        /// Create a branch linked to an issue.
        /// </summary>
        /// <param name="input">Parameters for CreateLinkedBranch</param>
        public CreateLinkedBranchPayload CreateLinkedBranch(Arg<CreateLinkedBranchInput> input) => this.CreateMethodCall(x => x.CreateLinkedBranch(input), Octokit.GraphQL.Model.CreateLinkedBranchPayload.Create);

        /// <summary>
        /// Creates a GitHub Enterprise Importer (GEI) migration source.
        /// </summary>
        /// <param name="input">Parameters for CreateMigrationSource</param>
        public CreateMigrationSourcePayload CreateMigrationSource(Arg<CreateMigrationSourceInput> input) => this.CreateMethodCall(x => x.CreateMigrationSource(input), Octokit.GraphQL.Model.CreateMigrationSourcePayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="input">Parameters for CreateProject</param>
        public CreateProjectPayload CreateProject(Arg<CreateProjectInput> input) => this.CreateMethodCall(x => x.CreateProject(input), Octokit.GraphQL.Model.CreateProjectPayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="input">Parameters for CreateProjectV2</param>
        public CreateProjectV2Payload CreateProjectV2(Arg<CreateProjectV2Input> input) => this.CreateMethodCall(x => x.CreateProjectV2(input), Octokit.GraphQL.Model.CreateProjectV2Payload.Create);

        /// <summary>
        /// Create a new project field.
        /// </summary>
        /// <param name="input">Parameters for CreateProjectV2Field</param>
        public CreateProjectV2FieldPayload CreateProjectV2Field(Arg<CreateProjectV2FieldInput> input) => this.CreateMethodCall(x => x.CreateProjectV2Field(input), Octokit.GraphQL.Model.CreateProjectV2FieldPayload.Create);

        /// <summary>
        /// Create a new pull request
        /// </summary>
        /// <param name="input">Parameters for CreatePullRequest</param>
        public CreatePullRequestPayload CreatePullRequest(Arg<CreatePullRequestInput> input) => this.CreateMethodCall(x => x.CreatePullRequest(input), Octokit.GraphQL.Model.CreatePullRequestPayload.Create);

        /// <summary>
        /// Create a new Git Ref.
        /// </summary>
        /// <param name="input">Parameters for CreateRef</param>
        public CreateRefPayload CreateRef(Arg<CreateRefInput> input) => this.CreateMethodCall(x => x.CreateRef(input), Octokit.GraphQL.Model.CreateRefPayload.Create);

        /// <summary>
        /// Create a new repository.
        /// </summary>
        /// <param name="input">Parameters for CreateRepository</param>
        public CreateRepositoryPayload CreateRepository(Arg<CreateRepositoryInput> input) => this.CreateMethodCall(x => x.CreateRepository(input), Octokit.GraphQL.Model.CreateRepositoryPayload.Create);

        /// <summary>
        /// Create a repository ruleset
        /// </summary>
        /// <param name="input">Parameters for CreateRepositoryRuleset</param>
        public CreateRepositoryRulesetPayload CreateRepositoryRuleset(Arg<CreateRepositoryRulesetInput> input) => this.CreateMethodCall(x => x.CreateRepositoryRuleset(input), Octokit.GraphQL.Model.CreateRepositoryRulesetPayload.Create);

        /// <summary>
        /// Create a GitHub Sponsors profile to allow others to sponsor you or your organization.
        /// </summary>
        /// <param name="input">Parameters for CreateSponsorsListing</param>
        public CreateSponsorsListingPayload CreateSponsorsListing(Arg<CreateSponsorsListingInput> input) => this.CreateMethodCall(x => x.CreateSponsorsListing(input), Octokit.GraphQL.Model.CreateSponsorsListingPayload.Create);

        /// <summary>
        /// Create a new payment tier for your GitHub Sponsors profile.
        /// </summary>
        /// <param name="input">Parameters for CreateSponsorsTier</param>
        public CreateSponsorsTierPayload CreateSponsorsTier(Arg<CreateSponsorsTierInput> input) => this.CreateMethodCall(x => x.CreateSponsorsTier(input), Octokit.GraphQL.Model.CreateSponsorsTierPayload.Create);

        /// <summary>
        /// Start a new sponsorship of a maintainer in GitHub Sponsors, or reactivate a past sponsorship.
        /// </summary>
        /// <param name="input">Parameters for CreateSponsorship</param>
        public CreateSponsorshipPayload CreateSponsorship(Arg<CreateSponsorshipInput> input) => this.CreateMethodCall(x => x.CreateSponsorship(input), Octokit.GraphQL.Model.CreateSponsorshipPayload.Create);

        /// <summary>
        /// Make many one-time sponsorships for different sponsorable users or organizations at once. Can only sponsor those who have a public GitHub Sponsors profile.
        /// </summary>
        /// <param name="input">Parameters for CreateSponsorships</param>
        public CreateSponsorshipsPayload CreateSponsorships(Arg<CreateSponsorshipsInput> input) => this.CreateMethodCall(x => x.CreateSponsorships(input), Octokit.GraphQL.Model.CreateSponsorshipsPayload.Create);

        /// <summary>
        /// Creates a new team discussion.
        /// </summary>
        /// <param name="input">Parameters for CreateTeamDiscussion</param>
        public CreateTeamDiscussionPayload CreateTeamDiscussion(Arg<CreateTeamDiscussionInput> input) => this.CreateMethodCall(x => x.CreateTeamDiscussion(input), Octokit.GraphQL.Model.CreateTeamDiscussionPayload.Create);

        /// <summary>
        /// Creates a new team discussion comment.
        /// </summary>
        /// <param name="input">Parameters for CreateTeamDiscussionComment</param>
        public CreateTeamDiscussionCommentPayload CreateTeamDiscussionComment(Arg<CreateTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.CreateTeamDiscussionComment(input), Octokit.GraphQL.Model.CreateTeamDiscussionCommentPayload.Create);

        /// <summary>
        /// Creates a new user list.
        /// </summary>
        /// <param name="input">Parameters for CreateUserList</param>
        public CreateUserListPayload CreateUserList(Arg<CreateUserListInput> input) => this.CreateMethodCall(x => x.CreateUserList(input), Octokit.GraphQL.Model.CreateUserListPayload.Create);

        /// <summary>
        /// Rejects a suggested topic for the repository.
        /// </summary>
        /// <param name="input">Parameters for DeclineTopicSuggestion</param>
        public DeclineTopicSuggestionPayload DeclineTopicSuggestion(Arg<DeclineTopicSuggestionInput> input) => this.CreateMethodCall(x => x.DeclineTopicSuggestion(input), Octokit.GraphQL.Model.DeclineTopicSuggestionPayload.Create);

        /// <summary>
        /// Delete a branch protection rule
        /// </summary>
        /// <param name="input">Parameters for DeleteBranchProtectionRule</param>
        public DeleteBranchProtectionRulePayload DeleteBranchProtectionRule(Arg<DeleteBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.DeleteBranchProtectionRule(input), Octokit.GraphQL.Model.DeleteBranchProtectionRulePayload.Create);

        /// <summary>
        /// Deletes a deployment.
        /// </summary>
        /// <param name="input">Parameters for DeleteDeployment</param>
        public DeleteDeploymentPayload DeleteDeployment(Arg<DeleteDeploymentInput> input) => this.CreateMethodCall(x => x.DeleteDeployment(input), Octokit.GraphQL.Model.DeleteDeploymentPayload.Create);

        /// <summary>
        /// Delete a discussion and all of its replies.
        /// </summary>
        /// <param name="input">Parameters for DeleteDiscussion</param>
        public DeleteDiscussionPayload DeleteDiscussion(Arg<DeleteDiscussionInput> input) => this.CreateMethodCall(x => x.DeleteDiscussion(input), Octokit.GraphQL.Model.DeleteDiscussionPayload.Create);

        /// <summary>
        /// Delete a discussion comment. If it has replies, wipe it instead.
        /// </summary>
        /// <param name="input">Parameters for DeleteDiscussionComment</param>
        public DeleteDiscussionCommentPayload DeleteDiscussionComment(Arg<DeleteDiscussionCommentInput> input) => this.CreateMethodCall(x => x.DeleteDiscussionComment(input), Octokit.GraphQL.Model.DeleteDiscussionCommentPayload.Create);

        /// <summary>
        /// Deletes an environment
        /// </summary>
        /// <param name="input">Parameters for DeleteEnvironment</param>
        public DeleteEnvironmentPayload DeleteEnvironment(Arg<DeleteEnvironmentInput> input) => this.CreateMethodCall(x => x.DeleteEnvironment(input), Octokit.GraphQL.Model.DeleteEnvironmentPayload.Create);

        /// <summary>
        /// Deletes an IP allow list entry.
        /// </summary>
        /// <param name="input">Parameters for DeleteIpAllowListEntry</param>
        public DeleteIpAllowListEntryPayload DeleteIpAllowListEntry(Arg<DeleteIpAllowListEntryInput> input) => this.CreateMethodCall(x => x.DeleteIpAllowListEntry(input), Octokit.GraphQL.Model.DeleteIpAllowListEntryPayload.Create);

        /// <summary>
        /// Deletes an Issue object.
        /// </summary>
        /// <param name="input">Parameters for DeleteIssue</param>
        public DeleteIssuePayload DeleteIssue(Arg<DeleteIssueInput> input) => this.CreateMethodCall(x => x.DeleteIssue(input), Octokit.GraphQL.Model.DeleteIssuePayload.Create);

        /// <summary>
        /// Deletes an IssueComment object.
        /// </summary>
        /// <param name="input">Parameters for DeleteIssueComment</param>
        public DeleteIssueCommentPayload DeleteIssueComment(Arg<DeleteIssueCommentInput> input) => this.CreateMethodCall(x => x.DeleteIssueComment(input), Octokit.GraphQL.Model.DeleteIssueCommentPayload.Create);

        /// <summary>
        /// Unlink a branch from an issue.
        /// </summary>
        /// <param name="input">Parameters for DeleteLinkedBranch</param>
        public DeleteLinkedBranchPayload DeleteLinkedBranch(Arg<DeleteLinkedBranchInput> input) => this.CreateMethodCall(x => x.DeleteLinkedBranch(input), Octokit.GraphQL.Model.DeleteLinkedBranchPayload.Create);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <param name="input">Parameters for DeleteProject</param>
        public DeleteProjectPayload DeleteProject(Arg<DeleteProjectInput> input) => this.CreateMethodCall(x => x.DeleteProject(input), Octokit.GraphQL.Model.DeleteProjectPayload.Create);

        /// <summary>
        /// Deletes a project card.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectCard</param>
        public DeleteProjectCardPayload DeleteProjectCard(Arg<DeleteProjectCardInput> input) => this.CreateMethodCall(x => x.DeleteProjectCard(input), Octokit.GraphQL.Model.DeleteProjectCardPayload.Create);

        /// <summary>
        /// Deletes a project column.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectColumn</param>
        public DeleteProjectColumnPayload DeleteProjectColumn(Arg<DeleteProjectColumnInput> input) => this.CreateMethodCall(x => x.DeleteProjectColumn(input), Octokit.GraphQL.Model.DeleteProjectColumnPayload.Create);

        /// <summary>
        /// Delete a project.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectV2</param>
        public DeleteProjectV2Payload DeleteProjectV2(Arg<DeleteProjectV2Input> input) => this.CreateMethodCall(x => x.DeleteProjectV2(input), Octokit.GraphQL.Model.DeleteProjectV2Payload.Create);

        /// <summary>
        /// Delete a project field.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectV2Field</param>
        public DeleteProjectV2FieldPayload DeleteProjectV2Field(Arg<DeleteProjectV2FieldInput> input) => this.CreateMethodCall(x => x.DeleteProjectV2Field(input), Octokit.GraphQL.Model.DeleteProjectV2FieldPayload.Create);

        /// <summary>
        /// Deletes an item from a Project.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectV2Item</param>
        public DeleteProjectV2ItemPayload DeleteProjectV2Item(Arg<DeleteProjectV2ItemInput> input) => this.CreateMethodCall(x => x.DeleteProjectV2Item(input), Octokit.GraphQL.Model.DeleteProjectV2ItemPayload.Create);

        /// <summary>
        /// Deletes a project workflow.
        /// </summary>
        /// <param name="input">Parameters for DeleteProjectV2Workflow</param>
        public DeleteProjectV2WorkflowPayload DeleteProjectV2Workflow(Arg<DeleteProjectV2WorkflowInput> input) => this.CreateMethodCall(x => x.DeleteProjectV2Workflow(input), Octokit.GraphQL.Model.DeleteProjectV2WorkflowPayload.Create);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <param name="input">Parameters for DeletePullRequestReview</param>
        public DeletePullRequestReviewPayload DeletePullRequestReview(Arg<DeletePullRequestReviewInput> input) => this.CreateMethodCall(x => x.DeletePullRequestReview(input), Octokit.GraphQL.Model.DeletePullRequestReviewPayload.Create);

        /// <summary>
        /// Deletes a pull request review comment.
        /// </summary>
        /// <param name="input">Parameters for DeletePullRequestReviewComment</param>
        public DeletePullRequestReviewCommentPayload DeletePullRequestReviewComment(Arg<DeletePullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.DeletePullRequestReviewComment(input), Octokit.GraphQL.Model.DeletePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Delete a Git Ref.
        /// </summary>
        /// <param name="input">Parameters for DeleteRef</param>
        public DeleteRefPayload DeleteRef(Arg<DeleteRefInput> input) => this.CreateMethodCall(x => x.DeleteRef(input), Octokit.GraphQL.Model.DeleteRefPayload.Create);

        /// <summary>
        /// Delete a repository ruleset
        /// </summary>
        /// <param name="input">Parameters for DeleteRepositoryRuleset</param>
        public DeleteRepositoryRulesetPayload DeleteRepositoryRuleset(Arg<DeleteRepositoryRulesetInput> input) => this.CreateMethodCall(x => x.DeleteRepositoryRuleset(input), Octokit.GraphQL.Model.DeleteRepositoryRulesetPayload.Create);

        /// <summary>
        /// Deletes a team discussion.
        /// </summary>
        /// <param name="input">Parameters for DeleteTeamDiscussion</param>
        public DeleteTeamDiscussionPayload DeleteTeamDiscussion(Arg<DeleteTeamDiscussionInput> input) => this.CreateMethodCall(x => x.DeleteTeamDiscussion(input), Octokit.GraphQL.Model.DeleteTeamDiscussionPayload.Create);

        /// <summary>
        /// Deletes a team discussion comment.
        /// </summary>
        /// <param name="input">Parameters for DeleteTeamDiscussionComment</param>
        public DeleteTeamDiscussionCommentPayload DeleteTeamDiscussionComment(Arg<DeleteTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.DeleteTeamDiscussionComment(input), Octokit.GraphQL.Model.DeleteTeamDiscussionCommentPayload.Create);

        /// <summary>
        /// Deletes a user list.
        /// </summary>
        /// <param name="input">Parameters for DeleteUserList</param>
        public DeleteUserListPayload DeleteUserList(Arg<DeleteUserListInput> input) => this.CreateMethodCall(x => x.DeleteUserList(input), Octokit.GraphQL.Model.DeleteUserListPayload.Create);

        /// <summary>
        /// Deletes a verifiable domain.
        /// </summary>
        /// <param name="input">Parameters for DeleteVerifiableDomain</param>
        public DeleteVerifiableDomainPayload DeleteVerifiableDomain(Arg<DeleteVerifiableDomainInput> input) => this.CreateMethodCall(x => x.DeleteVerifiableDomain(input), Octokit.GraphQL.Model.DeleteVerifiableDomainPayload.Create);

        /// <summary>
        /// Remove a pull request from the merge queue.
        /// </summary>
        /// <param name="input">Parameters for DequeuePullRequest</param>
        public DequeuePullRequestPayload DequeuePullRequest(Arg<DequeuePullRequestInput> input) => this.CreateMethodCall(x => x.DequeuePullRequest(input), Octokit.GraphQL.Model.DequeuePullRequestPayload.Create);

        /// <summary>
        /// Disable auto merge on the given pull request
        /// </summary>
        /// <param name="input">Parameters for DisablePullRequestAutoMerge</param>
        public DisablePullRequestAutoMergePayload DisablePullRequestAutoMerge(Arg<DisablePullRequestAutoMergeInput> input) => this.CreateMethodCall(x => x.DisablePullRequestAutoMerge(input), Octokit.GraphQL.Model.DisablePullRequestAutoMergePayload.Create);

        /// <summary>
        /// Dismisses an approved or rejected pull request review.
        /// </summary>
        /// <param name="input">Parameters for DismissPullRequestReview</param>
        public DismissPullRequestReviewPayload DismissPullRequestReview(Arg<DismissPullRequestReviewInput> input) => this.CreateMethodCall(x => x.DismissPullRequestReview(input), Octokit.GraphQL.Model.DismissPullRequestReviewPayload.Create);

        /// <summary>
        /// Dismisses the Dependabot alert.
        /// </summary>
        /// <param name="input">Parameters for DismissRepositoryVulnerabilityAlert</param>
        public DismissRepositoryVulnerabilityAlertPayload DismissRepositoryVulnerabilityAlert(Arg<DismissRepositoryVulnerabilityAlertInput> input) => this.CreateMethodCall(x => x.DismissRepositoryVulnerabilityAlert(input), Octokit.GraphQL.Model.DismissRepositoryVulnerabilityAlertPayload.Create);

        /// <summary>
        /// Enable the default auto-merge on a pull request.
        /// </summary>
        /// <param name="input">Parameters for EnablePullRequestAutoMerge</param>
        public EnablePullRequestAutoMergePayload EnablePullRequestAutoMerge(Arg<EnablePullRequestAutoMergeInput> input) => this.CreateMethodCall(x => x.EnablePullRequestAutoMerge(input), Octokit.GraphQL.Model.EnablePullRequestAutoMergePayload.Create);

        /// <summary>
        /// Add a pull request to the merge queue.
        /// </summary>
        /// <param name="input">Parameters for EnqueuePullRequest</param>
        public EnqueuePullRequestPayload EnqueuePullRequest(Arg<EnqueuePullRequestInput> input) => this.CreateMethodCall(x => x.EnqueuePullRequest(input), Octokit.GraphQL.Model.EnqueuePullRequestPayload.Create);

        /// <summary>
        /// Follow an organization.
        /// </summary>
        /// <param name="input">Parameters for FollowOrganization</param>
        public FollowOrganizationPayload FollowOrganization(Arg<FollowOrganizationInput> input) => this.CreateMethodCall(x => x.FollowOrganization(input), Octokit.GraphQL.Model.FollowOrganizationPayload.Create);

        /// <summary>
        /// Follow a user.
        /// </summary>
        /// <param name="input">Parameters for FollowUser</param>
        public FollowUserPayload FollowUser(Arg<FollowUserInput> input) => this.CreateMethodCall(x => x.FollowUser(input), Octokit.GraphQL.Model.FollowUserPayload.Create);

        /// <summary>
        /// Grant the migrator role to a user for all organizations under an enterprise account.
        /// </summary>
        /// <param name="input">Parameters for GrantEnterpriseOrganizationsMigratorRole</param>
        public GrantEnterpriseOrganizationsMigratorRolePayload GrantEnterpriseOrganizationsMigratorRole(Arg<GrantEnterpriseOrganizationsMigratorRoleInput> input) => this.CreateMethodCall(x => x.GrantEnterpriseOrganizationsMigratorRole(input), Octokit.GraphQL.Model.GrantEnterpriseOrganizationsMigratorRolePayload.Create);

        /// <summary>
        /// Grant the migrator role to a user or a team.
        /// </summary>
        /// <param name="input">Parameters for GrantMigratorRole</param>
        public GrantMigratorRolePayload GrantMigratorRole(Arg<GrantMigratorRoleInput> input) => this.CreateMethodCall(x => x.GrantMigratorRole(input), Octokit.GraphQL.Model.GrantMigratorRolePayload.Create);

        /// <summary>
        /// Invite someone to become an administrator of the enterprise.
        /// </summary>
        /// <param name="input">Parameters for InviteEnterpriseAdmin</param>
        public InviteEnterpriseAdminPayload InviteEnterpriseAdmin(Arg<InviteEnterpriseAdminInput> input) => this.CreateMethodCall(x => x.InviteEnterpriseAdmin(input), Octokit.GraphQL.Model.InviteEnterpriseAdminPayload.Create);

        /// <summary>
        /// Links a project to a repository.
        /// </summary>
        /// <param name="input">Parameters for LinkProjectV2ToRepository</param>
        public LinkProjectV2ToRepositoryPayload LinkProjectV2ToRepository(Arg<LinkProjectV2ToRepositoryInput> input) => this.CreateMethodCall(x => x.LinkProjectV2ToRepository(input), Octokit.GraphQL.Model.LinkProjectV2ToRepositoryPayload.Create);

        /// <summary>
        /// Links a project to a team.
        /// </summary>
        /// <param name="input">Parameters for LinkProjectV2ToTeam</param>
        public LinkProjectV2ToTeamPayload LinkProjectV2ToTeam(Arg<LinkProjectV2ToTeamInput> input) => this.CreateMethodCall(x => x.LinkProjectV2ToTeam(input), Octokit.GraphQL.Model.LinkProjectV2ToTeamPayload.Create);

        /// <summary>
        /// Creates a repository link for a project.
        /// </summary>
        /// <param name="input">Parameters for LinkRepositoryToProject</param>
        public LinkRepositoryToProjectPayload LinkRepositoryToProject(Arg<LinkRepositoryToProjectInput> input) => this.CreateMethodCall(x => x.LinkRepositoryToProject(input), Octokit.GraphQL.Model.LinkRepositoryToProjectPayload.Create);

        /// <summary>
        /// Lock a lockable object
        /// </summary>
        /// <param name="input">Parameters for LockLockable</param>
        public LockLockablePayload LockLockable(Arg<LockLockableInput> input) => this.CreateMethodCall(x => x.LockLockable(input), Octokit.GraphQL.Model.LockLockablePayload.Create);

        /// <summary>
        /// Mark a discussion comment as the chosen answer for discussions in an answerable category.
        /// </summary>
        /// <param name="input">Parameters for MarkDiscussionCommentAsAnswer</param>
        public MarkDiscussionCommentAsAnswerPayload MarkDiscussionCommentAsAnswer(Arg<MarkDiscussionCommentAsAnswerInput> input) => this.CreateMethodCall(x => x.MarkDiscussionCommentAsAnswer(input), Octokit.GraphQL.Model.MarkDiscussionCommentAsAnswerPayload.Create);

        /// <summary>
        /// Mark a pull request file as viewed
        /// </summary>
        /// <param name="input">Parameters for MarkFileAsViewed</param>
        public MarkFileAsViewedPayload MarkFileAsViewed(Arg<MarkFileAsViewedInput> input) => this.CreateMethodCall(x => x.MarkFileAsViewed(input), Octokit.GraphQL.Model.MarkFileAsViewedPayload.Create);

        /// <summary>
        /// Marks a notification as done
        /// </summary>
        /// <param name="input">Parameters for MarkNotificationAsDone</param>
        public MarkNotificationAsDonePayload MarkNotificationAsDone(Arg<MarkNotificationAsDoneInput> input) => this.CreateMethodCall(x => x.MarkNotificationAsDone(input), Octokit.GraphQL.Model.MarkNotificationAsDonePayload.Create);

        /// <summary>
        /// Mark a project as a template. Note that only projects which are owned by an Organization can be marked as a template.
        /// </summary>
        /// <param name="input">Parameters for MarkProjectV2AsTemplate</param>
        public MarkProjectV2AsTemplatePayload MarkProjectV2AsTemplate(Arg<MarkProjectV2AsTemplateInput> input) => this.CreateMethodCall(x => x.MarkProjectV2AsTemplate(input), Octokit.GraphQL.Model.MarkProjectV2AsTemplatePayload.Create);

        /// <summary>
        /// Marks a pull request ready for review.
        /// </summary>
        /// <param name="input">Parameters for MarkPullRequestReadyForReview</param>
        public MarkPullRequestReadyForReviewPayload MarkPullRequestReadyForReview(Arg<MarkPullRequestReadyForReviewInput> input) => this.CreateMethodCall(x => x.MarkPullRequestReadyForReview(input), Octokit.GraphQL.Model.MarkPullRequestReadyForReviewPayload.Create);

        /// <summary>
        /// Merge a head into a branch.
        /// </summary>
        /// <param name="input">Parameters for MergeBranch</param>
        public MergeBranchPayload MergeBranch(Arg<MergeBranchInput> input) => this.CreateMethodCall(x => x.MergeBranch(input), Octokit.GraphQL.Model.MergeBranchPayload.Create);

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <param name="input">Parameters for MergePullRequest</param>
        public MergePullRequestPayload MergePullRequest(Arg<MergePullRequestInput> input) => this.CreateMethodCall(x => x.MergePullRequest(input), Octokit.GraphQL.Model.MergePullRequestPayload.Create);

        /// <summary>
        /// Minimizes a comment on an Issue, Commit, Pull Request, or Gist
        /// </summary>
        /// <param name="input">Parameters for MinimizeComment</param>
        public MinimizeCommentPayload MinimizeComment(Arg<MinimizeCommentInput> input) => this.CreateMethodCall(x => x.MinimizeComment(input), Octokit.GraphQL.Model.MinimizeCommentPayload.Create);

        /// <summary>
        /// Moves a project card to another place.
        /// </summary>
        /// <param name="input">Parameters for MoveProjectCard</param>
        public MoveProjectCardPayload MoveProjectCard(Arg<MoveProjectCardInput> input) => this.CreateMethodCall(x => x.MoveProjectCard(input), Octokit.GraphQL.Model.MoveProjectCardPayload.Create);

        /// <summary>
        /// Moves a project column to another place.
        /// </summary>
        /// <param name="input">Parameters for MoveProjectColumn</param>
        public MoveProjectColumnPayload MoveProjectColumn(Arg<MoveProjectColumnInput> input) => this.CreateMethodCall(x => x.MoveProjectColumn(input), Octokit.GraphQL.Model.MoveProjectColumnPayload.Create);

        /// <summary>
        /// Pin an issue to a repository
        /// </summary>
        /// <param name="input">Parameters for PinIssue</param>
        public PinIssuePayload PinIssue(Arg<PinIssueInput> input) => this.CreateMethodCall(x => x.PinIssue(input), Octokit.GraphQL.Model.PinIssuePayload.Create);

        /// <summary>
        /// Publish an existing sponsorship tier that is currently still a draft to a GitHub Sponsors profile.
        /// </summary>
        /// <param name="input">Parameters for PublishSponsorsTier</param>
        public PublishSponsorsTierPayload PublishSponsorsTier(Arg<PublishSponsorsTierInput> input) => this.CreateMethodCall(x => x.PublishSponsorsTier(input), Octokit.GraphQL.Model.PublishSponsorsTierPayload.Create);

        /// <summary>
        /// Regenerates the identity provider recovery codes for an enterprise
        /// </summary>
        /// <param name="input">Parameters for RegenerateEnterpriseIdentityProviderRecoveryCodes</param>
        public RegenerateEnterpriseIdentityProviderRecoveryCodesPayload RegenerateEnterpriseIdentityProviderRecoveryCodes(Arg<RegenerateEnterpriseIdentityProviderRecoveryCodesInput> input) => this.CreateMethodCall(x => x.RegenerateEnterpriseIdentityProviderRecoveryCodes(input), Octokit.GraphQL.Model.RegenerateEnterpriseIdentityProviderRecoveryCodesPayload.Create);

        /// <summary>
        /// Regenerates a verifiable domain's verification token.
        /// </summary>
        /// <param name="input">Parameters for RegenerateVerifiableDomainToken</param>
        public RegenerateVerifiableDomainTokenPayload RegenerateVerifiableDomainToken(Arg<RegenerateVerifiableDomainTokenInput> input) => this.CreateMethodCall(x => x.RegenerateVerifiableDomainToken(input), Octokit.GraphQL.Model.RegenerateVerifiableDomainTokenPayload.Create);

        /// <summary>
        /// Reject all pending deployments under one or more environments
        /// </summary>
        /// <param name="input">Parameters for RejectDeployments</param>
        public RejectDeploymentsPayload RejectDeployments(Arg<RejectDeploymentsInput> input) => this.CreateMethodCall(x => x.RejectDeployments(input), Octokit.GraphQL.Model.RejectDeploymentsPayload.Create);

        /// <summary>
        /// Removes assignees from an assignable object.
        /// </summary>
        /// <param name="input">Parameters for RemoveAssigneesFromAssignable</param>
        public RemoveAssigneesFromAssignablePayload RemoveAssigneesFromAssignable(Arg<RemoveAssigneesFromAssignableInput> input) => this.CreateMethodCall(x => x.RemoveAssigneesFromAssignable(input), Octokit.GraphQL.Model.RemoveAssigneesFromAssignablePayload.Create);

        /// <summary>
        /// Removes an administrator from the enterprise.
        /// </summary>
        /// <param name="input">Parameters for RemoveEnterpriseAdmin</param>
        public RemoveEnterpriseAdminPayload RemoveEnterpriseAdmin(Arg<RemoveEnterpriseAdminInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseAdmin(input), Octokit.GraphQL.Model.RemoveEnterpriseAdminPayload.Create);

        /// <summary>
        /// Removes the identity provider from an enterprise
        /// </summary>
        /// <param name="input">Parameters for RemoveEnterpriseIdentityProvider</param>
        public RemoveEnterpriseIdentityProviderPayload RemoveEnterpriseIdentityProvider(Arg<RemoveEnterpriseIdentityProviderInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseIdentityProvider(input), Octokit.GraphQL.Model.RemoveEnterpriseIdentityProviderPayload.Create);

        /// <summary>
        /// Removes a user from all organizations within the enterprise
        /// </summary>
        /// <param name="input">Parameters for RemoveEnterpriseMember</param>
        public RemoveEnterpriseMemberPayload RemoveEnterpriseMember(Arg<RemoveEnterpriseMemberInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseMember(input), Octokit.GraphQL.Model.RemoveEnterpriseMemberPayload.Create);

        /// <summary>
        /// Removes an organization from the enterprise
        /// </summary>
        /// <param name="input">Parameters for RemoveEnterpriseOrganization</param>
        public RemoveEnterpriseOrganizationPayload RemoveEnterpriseOrganization(Arg<RemoveEnterpriseOrganizationInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseOrganization(input), Octokit.GraphQL.Model.RemoveEnterpriseOrganizationPayload.Create);

        /// <summary>
        /// Removes a support entitlement from an enterprise member.
        /// </summary>
        /// <param name="input">Parameters for RemoveEnterpriseSupportEntitlement</param>
        public RemoveEnterpriseSupportEntitlementPayload RemoveEnterpriseSupportEntitlement(Arg<RemoveEnterpriseSupportEntitlementInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseSupportEntitlement(input), Octokit.GraphQL.Model.RemoveEnterpriseSupportEntitlementPayload.Create);

        /// <summary>
        /// Removes labels from a Labelable object.
        /// </summary>
        /// <param name="input">Parameters for RemoveLabelsFromLabelable</param>
        public RemoveLabelsFromLabelablePayload RemoveLabelsFromLabelable(Arg<RemoveLabelsFromLabelableInput> input) => this.CreateMethodCall(x => x.RemoveLabelsFromLabelable(input), Octokit.GraphQL.Model.RemoveLabelsFromLabelablePayload.Create);

        /// <summary>
        /// Removes outside collaborator from all repositories in an organization.
        /// </summary>
        /// <param name="input">Parameters for RemoveOutsideCollaborator</param>
        public RemoveOutsideCollaboratorPayload RemoveOutsideCollaborator(Arg<RemoveOutsideCollaboratorInput> input) => this.CreateMethodCall(x => x.RemoveOutsideCollaborator(input), Octokit.GraphQL.Model.RemoveOutsideCollaboratorPayload.Create);

        /// <summary>
        /// Removes a reaction from a subject.
        /// </summary>
        /// <param name="input">Parameters for RemoveReaction</param>
        public RemoveReactionPayload RemoveReaction(Arg<RemoveReactionInput> input) => this.CreateMethodCall(x => x.RemoveReaction(input), Octokit.GraphQL.Model.RemoveReactionPayload.Create);

        /// <summary>
        /// Removes a star from a Starrable.
        /// </summary>
        /// <param name="input">Parameters for RemoveStar</param>
        public RemoveStarPayload RemoveStar(Arg<RemoveStarInput> input) => this.CreateMethodCall(x => x.RemoveStar(input), Octokit.GraphQL.Model.RemoveStarPayload.Create);

        /// <summary>
        /// Remove an upvote to a discussion or discussion comment.
        /// </summary>
        /// <param name="input">Parameters for RemoveUpvote</param>
        public RemoveUpvotePayload RemoveUpvote(Arg<RemoveUpvoteInput> input) => this.CreateMethodCall(x => x.RemoveUpvote(input), Octokit.GraphQL.Model.RemoveUpvotePayload.Create);

        /// <summary>
        /// Reopen a discussion.
        /// </summary>
        /// <param name="input">Parameters for ReopenDiscussion</param>
        public ReopenDiscussionPayload ReopenDiscussion(Arg<ReopenDiscussionInput> input) => this.CreateMethodCall(x => x.ReopenDiscussion(input), Octokit.GraphQL.Model.ReopenDiscussionPayload.Create);

        /// <summary>
        /// Reopen a issue.
        /// </summary>
        /// <param name="input">Parameters for ReopenIssue</param>
        public ReopenIssuePayload ReopenIssue(Arg<ReopenIssueInput> input) => this.CreateMethodCall(x => x.ReopenIssue(input), Octokit.GraphQL.Model.ReopenIssuePayload.Create);

        /// <summary>
        /// Reopen a pull request.
        /// </summary>
        /// <param name="input">Parameters for ReopenPullRequest</param>
        public ReopenPullRequestPayload ReopenPullRequest(Arg<ReopenPullRequestInput> input) => this.CreateMethodCall(x => x.ReopenPullRequest(input), Octokit.GraphQL.Model.ReopenPullRequestPayload.Create);

        /// <summary>
        /// Set review requests on a pull request.
        /// </summary>
        /// <param name="input">Parameters for RequestReviews</param>
        public RequestReviewsPayload RequestReviews(Arg<RequestReviewsInput> input) => this.CreateMethodCall(x => x.RequestReviews(input), Octokit.GraphQL.Model.RequestReviewsPayload.Create);

        /// <summary>
        /// Rerequests an existing check suite.
        /// </summary>
        /// <param name="input">Parameters for RerequestCheckSuite</param>
        public RerequestCheckSuitePayload RerequestCheckSuite(Arg<RerequestCheckSuiteInput> input) => this.CreateMethodCall(x => x.RerequestCheckSuite(input), Octokit.GraphQL.Model.RerequestCheckSuitePayload.Create);

        /// <summary>
        /// Marks a review thread as resolved.
        /// </summary>
        /// <param name="input">Parameters for ResolveReviewThread</param>
        public ResolveReviewThreadPayload ResolveReviewThread(Arg<ResolveReviewThreadInput> input) => this.CreateMethodCall(x => x.ResolveReviewThread(input), Octokit.GraphQL.Model.ResolveReviewThreadPayload.Create);

        /// <summary>
        /// Retire a published payment tier from your GitHub Sponsors profile so it cannot be used to start new sponsorships.
        /// </summary>
        /// <param name="input">Parameters for RetireSponsorsTier</param>
        public RetireSponsorsTierPayload RetireSponsorsTier(Arg<RetireSponsorsTierInput> input) => this.CreateMethodCall(x => x.RetireSponsorsTier(input), Octokit.GraphQL.Model.RetireSponsorsTierPayload.Create);

        /// <summary>
        /// Create a pull request that reverts the changes from a merged pull request.
        /// </summary>
        /// <param name="input">Parameters for RevertPullRequest</param>
        public RevertPullRequestPayload RevertPullRequest(Arg<RevertPullRequestInput> input) => this.CreateMethodCall(x => x.RevertPullRequest(input), Octokit.GraphQL.Model.RevertPullRequestPayload.Create);

        /// <summary>
        /// Revoke the migrator role to a user for all organizations under an enterprise account.
        /// </summary>
        /// <param name="input">Parameters for RevokeEnterpriseOrganizationsMigratorRole</param>
        public RevokeEnterpriseOrganizationsMigratorRolePayload RevokeEnterpriseOrganizationsMigratorRole(Arg<RevokeEnterpriseOrganizationsMigratorRoleInput> input) => this.CreateMethodCall(x => x.RevokeEnterpriseOrganizationsMigratorRole(input), Octokit.GraphQL.Model.RevokeEnterpriseOrganizationsMigratorRolePayload.Create);

        /// <summary>
        /// Revoke the migrator role from a user or a team.
        /// </summary>
        /// <param name="input">Parameters for RevokeMigratorRole</param>
        public RevokeMigratorRolePayload RevokeMigratorRole(Arg<RevokeMigratorRoleInput> input) => this.CreateMethodCall(x => x.RevokeMigratorRole(input), Octokit.GraphQL.Model.RevokeMigratorRolePayload.Create);

        /// <summary>
        /// Creates or updates the identity provider for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for SetEnterpriseIdentityProvider</param>
        public SetEnterpriseIdentityProviderPayload SetEnterpriseIdentityProvider(Arg<SetEnterpriseIdentityProviderInput> input) => this.CreateMethodCall(x => x.SetEnterpriseIdentityProvider(input), Octokit.GraphQL.Model.SetEnterpriseIdentityProviderPayload.Create);

        /// <summary>
        /// Set an organization level interaction limit for an organization's public repositories.
        /// </summary>
        /// <param name="input">Parameters for SetOrganizationInteractionLimit</param>
        public SetOrganizationInteractionLimitPayload SetOrganizationInteractionLimit(Arg<SetOrganizationInteractionLimitInput> input) => this.CreateMethodCall(x => x.SetOrganizationInteractionLimit(input), Octokit.GraphQL.Model.SetOrganizationInteractionLimitPayload.Create);

        /// <summary>
        /// Sets an interaction limit setting for a repository.
        /// </summary>
        /// <param name="input">Parameters for SetRepositoryInteractionLimit</param>
        public SetRepositoryInteractionLimitPayload SetRepositoryInteractionLimit(Arg<SetRepositoryInteractionLimitInput> input) => this.CreateMethodCall(x => x.SetRepositoryInteractionLimit(input), Octokit.GraphQL.Model.SetRepositoryInteractionLimitPayload.Create);

        /// <summary>
        /// Set a user level interaction limit for an user's public repositories.
        /// </summary>
        /// <param name="input">Parameters for SetUserInteractionLimit</param>
        public SetUserInteractionLimitPayload SetUserInteractionLimit(Arg<SetUserInteractionLimitInput> input) => this.CreateMethodCall(x => x.SetUserInteractionLimit(input), Octokit.GraphQL.Model.SetUserInteractionLimitPayload.Create);

        /// <summary>
        /// Starts a GitHub Enterprise Importer organization migration.
        /// </summary>
        /// <param name="input">Parameters for StartOrganizationMigration</param>
        public StartOrganizationMigrationPayload StartOrganizationMigration(Arg<StartOrganizationMigrationInput> input) => this.CreateMethodCall(x => x.StartOrganizationMigration(input), Octokit.GraphQL.Model.StartOrganizationMigrationPayload.Create);

        /// <summary>
        /// Starts a GitHub Enterprise Importer (GEI) repository migration.
        /// </summary>
        /// <param name="input">Parameters for StartRepositoryMigration</param>
        public StartRepositoryMigrationPayload StartRepositoryMigration(Arg<StartRepositoryMigrationInput> input) => this.CreateMethodCall(x => x.StartRepositoryMigration(input), Octokit.GraphQL.Model.StartRepositoryMigrationPayload.Create);

        /// <summary>
        /// Submits a pending pull request review.
        /// </summary>
        /// <param name="input">Parameters for SubmitPullRequestReview</param>
        public SubmitPullRequestReviewPayload SubmitPullRequestReview(Arg<SubmitPullRequestReviewInput> input) => this.CreateMethodCall(x => x.SubmitPullRequestReview(input), Octokit.GraphQL.Model.SubmitPullRequestReviewPayload.Create);

        /// <summary>
        /// Transfer an organization from one enterprise to another enterprise.
        /// </summary>
        /// <param name="input">Parameters for TransferEnterpriseOrganization</param>
        public TransferEnterpriseOrganizationPayload TransferEnterpriseOrganization(Arg<TransferEnterpriseOrganizationInput> input) => this.CreateMethodCall(x => x.TransferEnterpriseOrganization(input), Octokit.GraphQL.Model.TransferEnterpriseOrganizationPayload.Create);

        /// <summary>
        /// Transfer an issue to a different repository
        /// </summary>
        /// <param name="input">Parameters for TransferIssue</param>
        public TransferIssuePayload TransferIssue(Arg<TransferIssueInput> input) => this.CreateMethodCall(x => x.TransferIssue(input), Octokit.GraphQL.Model.TransferIssuePayload.Create);

        /// <summary>
        /// Unarchives a ProjectV2Item
        /// </summary>
        /// <param name="input">Parameters for UnarchiveProjectV2Item</param>
        public UnarchiveProjectV2ItemPayload UnarchiveProjectV2Item(Arg<UnarchiveProjectV2ItemInput> input) => this.CreateMethodCall(x => x.UnarchiveProjectV2Item(input), Octokit.GraphQL.Model.UnarchiveProjectV2ItemPayload.Create);

        /// <summary>
        /// Unarchives a repository.
        /// </summary>
        /// <param name="input">Parameters for UnarchiveRepository</param>
        public UnarchiveRepositoryPayload UnarchiveRepository(Arg<UnarchiveRepositoryInput> input) => this.CreateMethodCall(x => x.UnarchiveRepository(input), Octokit.GraphQL.Model.UnarchiveRepositoryPayload.Create);

        /// <summary>
        /// Unfollow an organization.
        /// </summary>
        /// <param name="input">Parameters for UnfollowOrganization</param>
        public UnfollowOrganizationPayload UnfollowOrganization(Arg<UnfollowOrganizationInput> input) => this.CreateMethodCall(x => x.UnfollowOrganization(input), Octokit.GraphQL.Model.UnfollowOrganizationPayload.Create);

        /// <summary>
        /// Unfollow a user.
        /// </summary>
        /// <param name="input">Parameters for UnfollowUser</param>
        public UnfollowUserPayload UnfollowUser(Arg<UnfollowUserInput> input) => this.CreateMethodCall(x => x.UnfollowUser(input), Octokit.GraphQL.Model.UnfollowUserPayload.Create);

        /// <summary>
        /// Unlinks a project from a repository.
        /// </summary>
        /// <param name="input">Parameters for UnlinkProjectV2FromRepository</param>
        public UnlinkProjectV2FromRepositoryPayload UnlinkProjectV2FromRepository(Arg<UnlinkProjectV2FromRepositoryInput> input) => this.CreateMethodCall(x => x.UnlinkProjectV2FromRepository(input), Octokit.GraphQL.Model.UnlinkProjectV2FromRepositoryPayload.Create);

        /// <summary>
        /// Unlinks a project to a team.
        /// </summary>
        /// <param name="input">Parameters for UnlinkProjectV2FromTeam</param>
        public UnlinkProjectV2FromTeamPayload UnlinkProjectV2FromTeam(Arg<UnlinkProjectV2FromTeamInput> input) => this.CreateMethodCall(x => x.UnlinkProjectV2FromTeam(input), Octokit.GraphQL.Model.UnlinkProjectV2FromTeamPayload.Create);

        /// <summary>
        /// Deletes a repository link from a project.
        /// </summary>
        /// <param name="input">Parameters for UnlinkRepositoryFromProject</param>
        public UnlinkRepositoryFromProjectPayload UnlinkRepositoryFromProject(Arg<UnlinkRepositoryFromProjectInput> input) => this.CreateMethodCall(x => x.UnlinkRepositoryFromProject(input), Octokit.GraphQL.Model.UnlinkRepositoryFromProjectPayload.Create);

        /// <summary>
        /// Unlock a lockable object
        /// </summary>
        /// <param name="input">Parameters for UnlockLockable</param>
        public UnlockLockablePayload UnlockLockable(Arg<UnlockLockableInput> input) => this.CreateMethodCall(x => x.UnlockLockable(input), Octokit.GraphQL.Model.UnlockLockablePayload.Create);

        /// <summary>
        /// Unmark a discussion comment as the chosen answer for discussions in an answerable category.
        /// </summary>
        /// <param name="input">Parameters for UnmarkDiscussionCommentAsAnswer</param>
        public UnmarkDiscussionCommentAsAnswerPayload UnmarkDiscussionCommentAsAnswer(Arg<UnmarkDiscussionCommentAsAnswerInput> input) => this.CreateMethodCall(x => x.UnmarkDiscussionCommentAsAnswer(input), Octokit.GraphQL.Model.UnmarkDiscussionCommentAsAnswerPayload.Create);

        /// <summary>
        /// Unmark a pull request file as viewed
        /// </summary>
        /// <param name="input">Parameters for UnmarkFileAsViewed</param>
        public UnmarkFileAsViewedPayload UnmarkFileAsViewed(Arg<UnmarkFileAsViewedInput> input) => this.CreateMethodCall(x => x.UnmarkFileAsViewed(input), Octokit.GraphQL.Model.UnmarkFileAsViewedPayload.Create);

        /// <summary>
        /// Unmark an issue as a duplicate of another issue.
        /// </summary>
        /// <param name="input">Parameters for UnmarkIssueAsDuplicate</param>
        public UnmarkIssueAsDuplicatePayload UnmarkIssueAsDuplicate(Arg<UnmarkIssueAsDuplicateInput> input) => this.CreateMethodCall(x => x.UnmarkIssueAsDuplicate(input), Octokit.GraphQL.Model.UnmarkIssueAsDuplicatePayload.Create);

        /// <summary>
        /// Unmark a project as a template.
        /// </summary>
        /// <param name="input">Parameters for UnmarkProjectV2AsTemplate</param>
        public UnmarkProjectV2AsTemplatePayload UnmarkProjectV2AsTemplate(Arg<UnmarkProjectV2AsTemplateInput> input) => this.CreateMethodCall(x => x.UnmarkProjectV2AsTemplate(input), Octokit.GraphQL.Model.UnmarkProjectV2AsTemplatePayload.Create);

        /// <summary>
        /// Unminimizes a comment on an Issue, Commit, Pull Request, or Gist
        /// </summary>
        /// <param name="input">Parameters for UnminimizeComment</param>
        public UnminimizeCommentPayload UnminimizeComment(Arg<UnminimizeCommentInput> input) => this.CreateMethodCall(x => x.UnminimizeComment(input), Octokit.GraphQL.Model.UnminimizeCommentPayload.Create);

        /// <summary>
        /// Unpin a pinned issue from a repository
        /// </summary>
        /// <param name="input">Parameters for UnpinIssue</param>
        public UnpinIssuePayload UnpinIssue(Arg<UnpinIssueInput> input) => this.CreateMethodCall(x => x.UnpinIssue(input), Octokit.GraphQL.Model.UnpinIssuePayload.Create);

        /// <summary>
        /// Marks a review thread as unresolved.
        /// </summary>
        /// <param name="input">Parameters for UnresolveReviewThread</param>
        public UnresolveReviewThreadPayload UnresolveReviewThread(Arg<UnresolveReviewThreadInput> input) => this.CreateMethodCall(x => x.UnresolveReviewThread(input), Octokit.GraphQL.Model.UnresolveReviewThreadPayload.Create);

        /// <summary>
        /// Unsubscribes from notifications
        /// </summary>
        /// <param name="input">Parameters for UnsubscribeFromNotifications</param>
        public UnsubscribeFromNotificationsPayload UnsubscribeFromNotifications(Arg<UnsubscribeFromNotificationsInput> input) => this.CreateMethodCall(x => x.UnsubscribeFromNotifications(input), Octokit.GraphQL.Model.UnsubscribeFromNotificationsPayload.Create);

        /// <summary>
        /// Update a branch protection rule
        /// </summary>
        /// <param name="input">Parameters for UpdateBranchProtectionRule</param>
        public UpdateBranchProtectionRulePayload UpdateBranchProtectionRule(Arg<UpdateBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.UpdateBranchProtectionRule(input), Octokit.GraphQL.Model.UpdateBranchProtectionRulePayload.Create);

        /// <summary>
        /// Update a check run
        /// </summary>
        /// <param name="input">Parameters for UpdateCheckRun</param>
        public UpdateCheckRunPayload UpdateCheckRun(Arg<UpdateCheckRunInput> input) => this.CreateMethodCall(x => x.UpdateCheckRun(input), Octokit.GraphQL.Model.UpdateCheckRunPayload.Create);

        /// <summary>
        /// Modifies the settings of an existing check suite
        /// </summary>
        /// <param name="input">Parameters for UpdateCheckSuitePreferences</param>
        public UpdateCheckSuitePreferencesPayload UpdateCheckSuitePreferences(Arg<UpdateCheckSuitePreferencesInput> input) => this.CreateMethodCall(x => x.UpdateCheckSuitePreferences(input), Octokit.GraphQL.Model.UpdateCheckSuitePreferencesPayload.Create);

        /// <summary>
        /// Update a discussion
        /// </summary>
        /// <param name="input">Parameters for UpdateDiscussion</param>
        public UpdateDiscussionPayload UpdateDiscussion(Arg<UpdateDiscussionInput> input) => this.CreateMethodCall(x => x.UpdateDiscussion(input), Octokit.GraphQL.Model.UpdateDiscussionPayload.Create);

        /// <summary>
        /// Update the contents of a comment on a Discussion
        /// </summary>
        /// <param name="input">Parameters for UpdateDiscussionComment</param>
        public UpdateDiscussionCommentPayload UpdateDiscussionComment(Arg<UpdateDiscussionCommentInput> input) => this.CreateMethodCall(x => x.UpdateDiscussionComment(input), Octokit.GraphQL.Model.UpdateDiscussionCommentPayload.Create);

        /// <summary>
        /// Updates the role of an enterprise administrator.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseAdministratorRole</param>
        public UpdateEnterpriseAdministratorRolePayload UpdateEnterpriseAdministratorRole(Arg<UpdateEnterpriseAdministratorRoleInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseAdministratorRole(input), Octokit.GraphQL.Model.UpdateEnterpriseAdministratorRolePayload.Create);

        /// <summary>
        /// Sets whether private repository forks are enabled for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseAllowPrivateRepositoryForkingSetting</param>
        public UpdateEnterpriseAllowPrivateRepositoryForkingSettingPayload UpdateEnterpriseAllowPrivateRepositoryForkingSetting(Arg<UpdateEnterpriseAllowPrivateRepositoryForkingSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseAllowPrivateRepositoryForkingSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseAllowPrivateRepositoryForkingSettingPayload.Create);

        /// <summary>
        /// Sets the base repository permission for organizations in an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseDefaultRepositoryPermissionSetting</param>
        public UpdateEnterpriseDefaultRepositoryPermissionSettingPayload UpdateEnterpriseDefaultRepositoryPermissionSetting(Arg<UpdateEnterpriseDefaultRepositoryPermissionSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseDefaultRepositoryPermissionSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseDefaultRepositoryPermissionSettingPayload.Create);

        /// <summary>
        /// Sets whether organization members with admin permissions on a repository can change repository visibility.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanChangeRepositoryVisibilitySetting</param>
        public UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingPayload UpdateEnterpriseMembersCanChangeRepositoryVisibilitySetting(Arg<UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanChangeRepositoryVisibilitySetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingPayload.Create);

        /// <summary>
        /// Sets the members can create repositories setting for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanCreateRepositoriesSetting</param>
        public UpdateEnterpriseMembersCanCreateRepositoriesSettingPayload UpdateEnterpriseMembersCanCreateRepositoriesSetting(Arg<UpdateEnterpriseMembersCanCreateRepositoriesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanCreateRepositoriesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanCreateRepositoriesSettingPayload.Create);

        /// <summary>
        /// Sets the members can delete issues setting for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanDeleteIssuesSetting</param>
        public UpdateEnterpriseMembersCanDeleteIssuesSettingPayload UpdateEnterpriseMembersCanDeleteIssuesSetting(Arg<UpdateEnterpriseMembersCanDeleteIssuesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanDeleteIssuesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanDeleteIssuesSettingPayload.Create);

        /// <summary>
        /// Sets the members can delete repositories setting for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanDeleteRepositoriesSetting</param>
        public UpdateEnterpriseMembersCanDeleteRepositoriesSettingPayload UpdateEnterpriseMembersCanDeleteRepositoriesSetting(Arg<UpdateEnterpriseMembersCanDeleteRepositoriesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanDeleteRepositoriesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanDeleteRepositoriesSettingPayload.Create);

        /// <summary>
        /// Sets whether members can invite collaborators are enabled for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanInviteCollaboratorsSetting</param>
        public UpdateEnterpriseMembersCanInviteCollaboratorsSettingPayload UpdateEnterpriseMembersCanInviteCollaboratorsSetting(Arg<UpdateEnterpriseMembersCanInviteCollaboratorsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanInviteCollaboratorsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanInviteCollaboratorsSettingPayload.Create);

        /// <summary>
        /// Sets whether or not an organization owner can make purchases.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanMakePurchasesSetting</param>
        public UpdateEnterpriseMembersCanMakePurchasesSettingPayload UpdateEnterpriseMembersCanMakePurchasesSetting(Arg<UpdateEnterpriseMembersCanMakePurchasesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanMakePurchasesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanMakePurchasesSettingPayload.Create);

        /// <summary>
        /// Sets the members can update protected branches setting for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanUpdateProtectedBranchesSetting</param>
        public UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingPayload UpdateEnterpriseMembersCanUpdateProtectedBranchesSetting(Arg<UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanUpdateProtectedBranchesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingPayload.Create);

        /// <summary>
        /// Sets the members can view dependency insights for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseMembersCanViewDependencyInsightsSetting</param>
        public UpdateEnterpriseMembersCanViewDependencyInsightsSettingPayload UpdateEnterpriseMembersCanViewDependencyInsightsSetting(Arg<UpdateEnterpriseMembersCanViewDependencyInsightsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanViewDependencyInsightsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanViewDependencyInsightsSettingPayload.Create);

        /// <summary>
        /// Sets whether organization projects are enabled for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseOrganizationProjectsSetting</param>
        public UpdateEnterpriseOrganizationProjectsSettingPayload UpdateEnterpriseOrganizationProjectsSetting(Arg<UpdateEnterpriseOrganizationProjectsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseOrganizationProjectsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseOrganizationProjectsSettingPayload.Create);

        /// <summary>
        /// Updates the role of an enterprise owner with an organization.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseOwnerOrganizationRole</param>
        public UpdateEnterpriseOwnerOrganizationRolePayload UpdateEnterpriseOwnerOrganizationRole(Arg<UpdateEnterpriseOwnerOrganizationRoleInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseOwnerOrganizationRole(input), Octokit.GraphQL.Model.UpdateEnterpriseOwnerOrganizationRolePayload.Create);

        /// <summary>
        /// Updates an enterprise's profile.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseProfile</param>
        public UpdateEnterpriseProfilePayload UpdateEnterpriseProfile(Arg<UpdateEnterpriseProfileInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseProfile(input), Octokit.GraphQL.Model.UpdateEnterpriseProfilePayload.Create);

        /// <summary>
        /// Sets whether repository projects are enabled for a enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseRepositoryProjectsSetting</param>
        public UpdateEnterpriseRepositoryProjectsSettingPayload UpdateEnterpriseRepositoryProjectsSetting(Arg<UpdateEnterpriseRepositoryProjectsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseRepositoryProjectsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseRepositoryProjectsSettingPayload.Create);

        /// <summary>
        /// Sets whether team discussions are enabled for an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseTeamDiscussionsSetting</param>
        public UpdateEnterpriseTeamDiscussionsSettingPayload UpdateEnterpriseTeamDiscussionsSetting(Arg<UpdateEnterpriseTeamDiscussionsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseTeamDiscussionsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseTeamDiscussionsSettingPayload.Create);

        /// <summary>
        /// Sets whether two factor authentication is required for all users in an enterprise.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnterpriseTwoFactorAuthenticationRequiredSetting</param>
        public UpdateEnterpriseTwoFactorAuthenticationRequiredSettingPayload UpdateEnterpriseTwoFactorAuthenticationRequiredSetting(Arg<UpdateEnterpriseTwoFactorAuthenticationRequiredSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseTwoFactorAuthenticationRequiredSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseTwoFactorAuthenticationRequiredSettingPayload.Create);

        /// <summary>
        /// Updates an environment.
        /// </summary>
        /// <param name="input">Parameters for UpdateEnvironment</param>
        public UpdateEnvironmentPayload UpdateEnvironment(Arg<UpdateEnvironmentInput> input) => this.CreateMethodCall(x => x.UpdateEnvironment(input), Octokit.GraphQL.Model.UpdateEnvironmentPayload.Create);

        /// <summary>
        /// Sets whether an IP allow list is enabled on an owner.
        /// </summary>
        /// <param name="input">Parameters for UpdateIpAllowListEnabledSetting</param>
        public UpdateIpAllowListEnabledSettingPayload UpdateIpAllowListEnabledSetting(Arg<UpdateIpAllowListEnabledSettingInput> input) => this.CreateMethodCall(x => x.UpdateIpAllowListEnabledSetting(input), Octokit.GraphQL.Model.UpdateIpAllowListEnabledSettingPayload.Create);

        /// <summary>
        /// Updates an IP allow list entry.
        /// </summary>
        /// <param name="input">Parameters for UpdateIpAllowListEntry</param>
        public UpdateIpAllowListEntryPayload UpdateIpAllowListEntry(Arg<UpdateIpAllowListEntryInput> input) => this.CreateMethodCall(x => x.UpdateIpAllowListEntry(input), Octokit.GraphQL.Model.UpdateIpAllowListEntryPayload.Create);

        /// <summary>
        /// Sets whether IP allow list configuration for installed GitHub Apps is enabled on an owner.
        /// </summary>
        /// <param name="input">Parameters for UpdateIpAllowListForInstalledAppsEnabledSetting</param>
        public UpdateIpAllowListForInstalledAppsEnabledSettingPayload UpdateIpAllowListForInstalledAppsEnabledSetting(Arg<UpdateIpAllowListForInstalledAppsEnabledSettingInput> input) => this.CreateMethodCall(x => x.UpdateIpAllowListForInstalledAppsEnabledSetting(input), Octokit.GraphQL.Model.UpdateIpAllowListForInstalledAppsEnabledSettingPayload.Create);

        /// <summary>
        /// Updates an Issue.
        /// </summary>
        /// <param name="input">Parameters for UpdateIssue</param>
        public UpdateIssuePayload UpdateIssue(Arg<UpdateIssueInput> input) => this.CreateMethodCall(x => x.UpdateIssue(input), Octokit.GraphQL.Model.UpdateIssuePayload.Create);

        /// <summary>
        /// Updates an IssueComment object.
        /// </summary>
        /// <param name="input">Parameters for UpdateIssueComment</param>
        public UpdateIssueCommentPayload UpdateIssueComment(Arg<UpdateIssueCommentInput> input) => this.CreateMethodCall(x => x.UpdateIssueComment(input), Octokit.GraphQL.Model.UpdateIssueCommentPayload.Create);

        /// <summary>
        /// Update the setting to restrict notifications to only verified or approved domains available to an owner.
        /// </summary>
        /// <param name="input">Parameters for UpdateNotificationRestrictionSetting</param>
        public UpdateNotificationRestrictionSettingPayload UpdateNotificationRestrictionSetting(Arg<UpdateNotificationRestrictionSettingInput> input) => this.CreateMethodCall(x => x.UpdateNotificationRestrictionSetting(input), Octokit.GraphQL.Model.UpdateNotificationRestrictionSettingPayload.Create);

        /// <summary>
        /// Sets whether private repository forks are enabled for an organization.
        /// </summary>
        /// <param name="input">Parameters for UpdateOrganizationAllowPrivateRepositoryForkingSetting</param>
        public UpdateOrganizationAllowPrivateRepositoryForkingSettingPayload UpdateOrganizationAllowPrivateRepositoryForkingSetting(Arg<UpdateOrganizationAllowPrivateRepositoryForkingSettingInput> input) => this.CreateMethodCall(x => x.UpdateOrganizationAllowPrivateRepositoryForkingSetting(input), Octokit.GraphQL.Model.UpdateOrganizationAllowPrivateRepositoryForkingSettingPayload.Create);

        /// <summary>
        /// Sets whether contributors are required to sign off on web-based commits for repositories in an organization.
        /// </summary>
        /// <param name="input">Parameters for UpdateOrganizationWebCommitSignoffSetting</param>
        public UpdateOrganizationWebCommitSignoffSettingPayload UpdateOrganizationWebCommitSignoffSetting(Arg<UpdateOrganizationWebCommitSignoffSettingInput> input) => this.CreateMethodCall(x => x.UpdateOrganizationWebCommitSignoffSetting(input), Octokit.GraphQL.Model.UpdateOrganizationWebCommitSignoffSettingPayload.Create);

        /// <summary>
        /// Toggle the setting for your GitHub Sponsors profile that allows other GitHub accounts to sponsor you on GitHub while paying for the sponsorship on Patreon. Only applicable when you have a GitHub Sponsors profile and have connected your GitHub account with Patreon.
        /// </summary>
        /// <param name="input">Parameters for UpdatePatreonSponsorability</param>
        public UpdatePatreonSponsorabilityPayload UpdatePatreonSponsorability(Arg<UpdatePatreonSponsorabilityInput> input) => this.CreateMethodCall(x => x.UpdatePatreonSponsorability(input), Octokit.GraphQL.Model.UpdatePatreonSponsorabilityPayload.Create);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="input">Parameters for UpdateProject</param>
        public UpdateProjectPayload UpdateProject(Arg<UpdateProjectInput> input) => this.CreateMethodCall(x => x.UpdateProject(input), Octokit.GraphQL.Model.UpdateProjectPayload.Create);

        /// <summary>
        /// Updates an existing project card.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectCard</param>
        public UpdateProjectCardPayload UpdateProjectCard(Arg<UpdateProjectCardInput> input) => this.CreateMethodCall(x => x.UpdateProjectCard(input), Octokit.GraphQL.Model.UpdateProjectCardPayload.Create);

        /// <summary>
        /// Updates an existing project column.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectColumn</param>
        public UpdateProjectColumnPayload UpdateProjectColumn(Arg<UpdateProjectColumnInput> input) => this.CreateMethodCall(x => x.UpdateProjectColumn(input), Octokit.GraphQL.Model.UpdateProjectColumnPayload.Create);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectV2</param>
        public UpdateProjectV2Payload UpdateProjectV2(Arg<UpdateProjectV2Input> input) => this.CreateMethodCall(x => x.UpdateProjectV2(input), Octokit.GraphQL.Model.UpdateProjectV2Payload.Create);

        /// <summary>
        /// Update the collaborators on a team or a project
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectV2Collaborators</param>
        public UpdateProjectV2CollaboratorsPayload UpdateProjectV2Collaborators(Arg<UpdateProjectV2CollaboratorsInput> input) => this.CreateMethodCall(x => x.UpdateProjectV2Collaborators(input), Octokit.GraphQL.Model.UpdateProjectV2CollaboratorsPayload.Create);

        /// <summary>
        /// Updates a draft issue within a Project.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectV2DraftIssue</param>
        public UpdateProjectV2DraftIssuePayload UpdateProjectV2DraftIssue(Arg<UpdateProjectV2DraftIssueInput> input) => this.CreateMethodCall(x => x.UpdateProjectV2DraftIssue(input), Octokit.GraphQL.Model.UpdateProjectV2DraftIssuePayload.Create);

        /// <summary>
        /// This mutation updates the value of a field for an item in a Project. Currently only single-select, text, number, date, and iteration fields are supported.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectV2ItemFieldValue</param>
        public UpdateProjectV2ItemFieldValuePayload UpdateProjectV2ItemFieldValue(Arg<UpdateProjectV2ItemFieldValueInput> input) => this.CreateMethodCall(x => x.UpdateProjectV2ItemFieldValue(input), Octokit.GraphQL.Model.UpdateProjectV2ItemFieldValuePayload.Create);

        /// <summary>
        /// This mutation updates the position of the item in the project, where the position represents the priority of an item.
        /// </summary>
        /// <param name="input">Parameters for UpdateProjectV2ItemPosition</param>
        public UpdateProjectV2ItemPositionPayload UpdateProjectV2ItemPosition(Arg<UpdateProjectV2ItemPositionInput> input) => this.CreateMethodCall(x => x.UpdateProjectV2ItemPosition(input), Octokit.GraphQL.Model.UpdateProjectV2ItemPositionPayload.Create);

        /// <summary>
        /// Update a pull request
        /// </summary>
        /// <param name="input">Parameters for UpdatePullRequest</param>
        public UpdatePullRequestPayload UpdatePullRequest(Arg<UpdatePullRequestInput> input) => this.CreateMethodCall(x => x.UpdatePullRequest(input), Octokit.GraphQL.Model.UpdatePullRequestPayload.Create);

        /// <summary>
        /// Merge or Rebase HEAD from upstream branch into pull request branch
        /// </summary>
        /// <param name="input">Parameters for UpdatePullRequestBranch</param>
        public UpdatePullRequestBranchPayload UpdatePullRequestBranch(Arg<UpdatePullRequestBranchInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestBranch(input), Octokit.GraphQL.Model.UpdatePullRequestBranchPayload.Create);

        /// <summary>
        /// Updates the body of a pull request review.
        /// </summary>
        /// <param name="input">Parameters for UpdatePullRequestReview</param>
        public UpdatePullRequestReviewPayload UpdatePullRequestReview(Arg<UpdatePullRequestReviewInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReview(input), Octokit.GraphQL.Model.UpdatePullRequestReviewPayload.Create);

        /// <summary>
        /// Updates a pull request review comment.
        /// </summary>
        /// <param name="input">Parameters for UpdatePullRequestReviewComment</param>
        public UpdatePullRequestReviewCommentPayload UpdatePullRequestReviewComment(Arg<UpdatePullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReviewComment(input), Octokit.GraphQL.Model.UpdatePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Update a Git Ref.
        /// </summary>
        /// <param name="input">Parameters for UpdateRef</param>
        public UpdateRefPayload UpdateRef(Arg<UpdateRefInput> input) => this.CreateMethodCall(x => x.UpdateRef(input), Octokit.GraphQL.Model.UpdateRefPayload.Create);

        /// <summary>
        /// Update information about a repository.
        /// </summary>
        /// <param name="input">Parameters for UpdateRepository</param>
        public UpdateRepositoryPayload UpdateRepository(Arg<UpdateRepositoryInput> input) => this.CreateMethodCall(x => x.UpdateRepository(input), Octokit.GraphQL.Model.UpdateRepositoryPayload.Create);

        /// <summary>
        /// Update a repository ruleset
        /// </summary>
        /// <param name="input">Parameters for UpdateRepositoryRuleset</param>
        public UpdateRepositoryRulesetPayload UpdateRepositoryRuleset(Arg<UpdateRepositoryRulesetInput> input) => this.CreateMethodCall(x => x.UpdateRepositoryRuleset(input), Octokit.GraphQL.Model.UpdateRepositoryRulesetPayload.Create);

        /// <summary>
        /// Sets whether contributors are required to sign off on web-based commits for a repository.
        /// </summary>
        /// <param name="input">Parameters for UpdateRepositoryWebCommitSignoffSetting</param>
        public UpdateRepositoryWebCommitSignoffSettingPayload UpdateRepositoryWebCommitSignoffSetting(Arg<UpdateRepositoryWebCommitSignoffSettingInput> input) => this.CreateMethodCall(x => x.UpdateRepositoryWebCommitSignoffSetting(input), Octokit.GraphQL.Model.UpdateRepositoryWebCommitSignoffSettingPayload.Create);

        /// <summary>
        /// Change visibility of your sponsorship and opt in or out of email updates from the maintainer.
        /// </summary>
        /// <param name="input">Parameters for UpdateSponsorshipPreferences</param>
        public UpdateSponsorshipPreferencesPayload UpdateSponsorshipPreferences(Arg<UpdateSponsorshipPreferencesInput> input) => this.CreateMethodCall(x => x.UpdateSponsorshipPreferences(input), Octokit.GraphQL.Model.UpdateSponsorshipPreferencesPayload.Create);

        /// <summary>
        /// Updates the state for subscribable subjects.
        /// </summary>
        /// <param name="input">Parameters for UpdateSubscription</param>
        public UpdateSubscriptionPayload UpdateSubscription(Arg<UpdateSubscriptionInput> input) => this.CreateMethodCall(x => x.UpdateSubscription(input), Octokit.GraphQL.Model.UpdateSubscriptionPayload.Create);

        /// <summary>
        /// Updates a team discussion.
        /// </summary>
        /// <param name="input">Parameters for UpdateTeamDiscussion</param>
        public UpdateTeamDiscussionPayload UpdateTeamDiscussion(Arg<UpdateTeamDiscussionInput> input) => this.CreateMethodCall(x => x.UpdateTeamDiscussion(input), Octokit.GraphQL.Model.UpdateTeamDiscussionPayload.Create);

        /// <summary>
        /// Updates a discussion comment.
        /// </summary>
        /// <param name="input">Parameters for UpdateTeamDiscussionComment</param>
        public UpdateTeamDiscussionCommentPayload UpdateTeamDiscussionComment(Arg<UpdateTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.UpdateTeamDiscussionComment(input), Octokit.GraphQL.Model.UpdateTeamDiscussionCommentPayload.Create);

        /// <summary>
        /// Update team repository.
        /// </summary>
        /// <param name="input">Parameters for UpdateTeamsRepository</param>
        public UpdateTeamsRepositoryPayload UpdateTeamsRepository(Arg<UpdateTeamsRepositoryInput> input) => this.CreateMethodCall(x => x.UpdateTeamsRepository(input), Octokit.GraphQL.Model.UpdateTeamsRepositoryPayload.Create);

        /// <summary>
        /// Replaces the repository's topics with the given topics.
        /// </summary>
        /// <param name="input">Parameters for UpdateTopics</param>
        public UpdateTopicsPayload UpdateTopics(Arg<UpdateTopicsInput> input) => this.CreateMethodCall(x => x.UpdateTopics(input), Octokit.GraphQL.Model.UpdateTopicsPayload.Create);

        /// <summary>
        /// Updates an existing user list.
        /// </summary>
        /// <param name="input">Parameters for UpdateUserList</param>
        public UpdateUserListPayload UpdateUserList(Arg<UpdateUserListInput> input) => this.CreateMethodCall(x => x.UpdateUserList(input), Octokit.GraphQL.Model.UpdateUserListPayload.Create);

        /// <summary>
        /// Updates which of the viewer's lists an item belongs to
        /// </summary>
        /// <param name="input">Parameters for UpdateUserListsForItem</param>
        public UpdateUserListsForItemPayload UpdateUserListsForItem(Arg<UpdateUserListsForItemInput> input) => this.CreateMethodCall(x => x.UpdateUserListsForItem(input), Octokit.GraphQL.Model.UpdateUserListsForItemPayload.Create);

        /// <summary>
        /// Verify that a verifiable domain has the expected DNS record.
        /// </summary>
        /// <param name="input">Parameters for VerifyVerifiableDomain</param>
        public VerifyVerifiableDomainPayload VerifyVerifiableDomain(Arg<VerifyVerifiableDomainInput> input) => this.CreateMethodCall(x => x.VerifyVerifiableDomain(input), Octokit.GraphQL.Model.VerifyVerifiableDomainPayload.Create);

        internal static Mutation Create(Expression expression)
        {
            return new Mutation(expression);
        }
    }
}