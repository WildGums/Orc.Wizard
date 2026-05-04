# Orc.Wizard

Orc.Wizard is a library for easily creating beautifully looking wizards for WPF using MVVM (powered by [Catel](https://github.com/catel/catel)).

The solution consists of the following projects:

- **Orc.Wizard** — Core wizard library (WPF controls, models, services, and navigation).
- **Orc.Wizard.Example** — Example application demonstrating how to build wizards with this library.
- **Orc.Wizard.Tests** — Unit and integration tests.

---

## Critical Rules (Read First)

These rules are **non-negotiable**. Violating them causes broken builds, crashes, or downstream breakage.

### 1. Never Edit Generated Files

Files matching `*.generated.cs` are auto-generated.

- **NEVER** manually edit these files

### 2. ABI / API Stability

This project maintains stable ABI / API. Breaking changes break downstream apps.

| Allowed | Never |
|---------|-------|
| Add new overloads | Modify existing signatures |
| Add new methods | Remove public APIs |
| Add new classes | Change return types |

### 3. Tests Are Mandatory

**Building alone is NOT sufficient.** Run tests before claiming completion (see [Commands](#commands)).

### 4. Branch Protection (COMPLIANCE REQUIRED)

**Direct commits to protected branches are a policy violation.**

| Repository | Protected Branches |
|------------|-------------------|
| Orc.Wizard | `master` |
| Orc.Wizard | `develop` |

**Required workflow:**

1. **Create a feature branch FIRST** — Use naming convention: `feature/issue-NNNN-description`
2. **Make all commits on the feature branch** — Never commit directly to protected branches
3. **Submit a Pull Request** — Changes must be reviewed by a human before merging

```bash
# CORRECT — Always create a feature branch first
git checkout -b feature/issue-1234-fix-description

# NEVER DO THIS — Policy violation
git checkout develop && git commit  # FORBIDDEN

# NEVER DO THIS — Policy violation
git checkout master && git commit  # FORBIDDEN
```

The repository has protected branches that must be respected.

---

## Commands

Single source of truth for all commands:

| Task | Command |
|------|---------|
| **Build** | `dotnet cake --target=build` |
| **Test** | `dotnet cake --target=test` |
| **Build and test** | `dotnet cake --target=buildandtest` |

---

## Architecture & Directories

### Layer Overview

```
Orc.Wizard           => Core WPF wizard library (controls, models, services, navigation)
Orc.Wizard.Example   => Example WPF application
Orc.Wizard.Tests     => NUnit test project
```

### Directory Guide

| Directory / File | Editable? | Notes |
|-----------------|-----------|-------|
| `*.generated.cs` | No | Leave as-is — auto-generated |
| `deployment/` | No | Deployment / build scripts |
| `src/Orc.Wizard/Models/` | Yes | Wizard and page model base classes |
| `src/Orc.Wizard/Services/` | Yes | `IWizardService` and implementation |
| `src/Orc.Wizard/ViewModels/` | Yes | View models for wizard host and pages |
| `src/Orc.Wizard/Views/` | Yes | WPF views / user controls |
| `src/Orc.Wizard/Controls/` | Yes | Reusable WPF controls |
| `src/Orc.Wizard/Navigation/` | Yes | Navigation strategies and controllers |
| `src/Orc.Wizard/Themes/` | Yes | WPF resource dictionaries and styles |
| `src/Orc.Wizard.Tests/` | Yes | Tests — keep in sync with source changes |

---

## Writing Code

### Key Patterns

- All models inherit from Catel's `ModelBase` or `WizardBase` / `WizardPageBase`.
- View models follow the Catel MVVM pattern — use `ViewModelBase` as the base class.
- Dependency injection uses `Microsoft.Extensions.DependencyInjection` via `IServiceProvider`.
- Use `Catel.Logging` / `Microsoft.Extensions.Logging` for logging.

### Anti-Patterns (Never Do This)

| Anti-Pattern | Why |
|-------------|-----|
| Modifying method signatures | ABI breaking |
| Manual edits to `*.generated.cs` | Overwritten on regenerate |
| Using default parameters in public APIs | ABI breaking |
| **Skipping failing tests** | **Unacceptable — tests must pass** |

---

## Testing & Debugging

### Running Tests

```bash
dotnet cake --target=test
```

### Tests MUST Pass

> **NON-NEGOTIABLE:** Tests must PASS before claiming completion.
>
> - Do NOT skip failing tests
> - Do NOT claim completion if tests fail
> - Do NOT use `SkipException` to work around failures

### Writing Tests

1. Use NUnit to write tests.
2. Combine Pascal / Snake case for test methods (e.g. `Feature_Does_Work`).
3. Public API changes must update the snapshot in `PublicApiFacts.Orc_Wizard_HasNoBreakingChanges_Async.verified.txt`.

```csharp
[Test]
public void Feature_Does_Work()
{
    var result = 47 - 5;

    Assert.That(result, Is.EqualTo(42));
}
```

**Philosophy:** Tests FAIL when wrong — never skip (except missing hardware).

### Debugging Methodology

1. **Establish baseline** — What's the known-good state?
2. **One change at a time** — Verify each change before proceeding
3. **Track changes in a table** — Log what you changed and the result
4. **Platform differences are signals** — If X works and Y fails, the difference IS the answer
5. **Revert if worse** — Don't pile fixes on top of failures
