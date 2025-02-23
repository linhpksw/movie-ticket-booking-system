# Blazor ASP.NET Project - Git Workflow Guide

## 1. Git Branching Rules
### Protected Branches:
- `master` and `develop` branches are protected.
- No direct commits are allowed to these branches.

### Workflow:
1. **Pull Latest Develop Branch**
   ```sh
   git checkout develop
   git pull origin develop
   ```
2. **Create a Feature Branch**
   ```sh
   git checkout -b feature/your-feature-name
   ```
3. **Commit Regularly**
   - Make frequent commits to help with rollback if needed.
   - Use meaningful commit messages (see below for guidelines).
4. **Push Your Feature Branch**
   ```sh
   git push origin feature/your-feature-name
   ```
5. **Create a Pull Request (PR)**
   - PR should target the `develop` branch.
   - Include a clear description of the feature.

## 2. Rebasing Before Merging
### If `develop` branch has changed since your last pull:
1. **Switch to Your Feature Branch**
   ```sh
   git checkout feature/your-feature-name
   ```
2. **Rebase with Latest Develop**
   ```sh
   git pull origin develop --rebase
   ```
3. **Resolve Conflicts (if any)**

4. **Test Your Feature Again**
   - Ensure all features still work correctly after rebasing.

5. **Push the Updated Branch**
   ```sh
   git push origin feature/your-feature-name
   ```
   - No need to recreate the PR; GitHub will update it automatically.

## 3. Git Commit Message Convention
### Follow a clear and structured commit message format:
```
[Type] [JIRA-ID] Short description
```
#### Examples:
- `feat: [JIRA-123] Implement user authentication`
- `fix: [JIRA-124] Resolve payment bug`
- `docs: [JIRA-125] Update README instructions`

### Common Commit Types:
- `feat:` (Feature addition)
- `fix:` (Bug fix)
- `docs:` (Documentation update)
- `refactor:` (Code improvement)
- `test:` (Adding tests)
- `chore:` (Miscellaneous tasks)

## 4. Feature Branch Naming Convention
- All feature branches should be created under `feature/`
- Naming format: `feature/short-description`
- Example: `feature/user-login`

## 5. Final Checks Before Merging
1. Ensure the latest `develop` is merged.
2. Rebase if required.
3. Test the application functionality.
4. Get approval from at least one team member before merging.

By following these steps, we maintain a clean and efficient development workflow for our Blazor ASP.NET project.

