import IFile from '../../interfaces/IFile'

interface ICheckOwnerAndPackageNameDefinitionsAreQuotedParams {
  files: IFile[];
  packagePrefix: string;
}

export function checkOwnerAndPackageNameDefinitionsAreQuoted ({
  files,
  packagePrefix
}: ICheckOwnerAndPackageNameDefinitionsAreQuotedParams): void {
  files.forEach((file) => {
    const lines = file.editableContent.split('\n')

    const definitionLines = lines.filter((line) =>
      line.trim().match(/CREATE/i)
    )

    const packagesDefinitionsAreQuoted = definitionLines.every(
      (definitionLine) => {
        const [packageBlock] = definitionLine
          .split(' ')
          .filter((definitionBlock) => definitionBlock.includes(packagePrefix))

        const [quotedOwner, quotedPackageName] = packageBlock.split('.')

        const ownerAndPackageAreQuoted =
          quotedOwner.match(/"/g)?.length === 2 &&
          quotedPackageName.match(/"/g)?.length === 2

        return ownerAndPackageAreQuoted
      }
    )

    !packagesDefinitionsAreQuoted &&
      file.errors.push(
        '❌ A package não possui aspas no owner e nome nas definições.'
      )
  })
}
