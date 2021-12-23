import IFile from '../../interfaces/IFile'

interface ICheckOwnerIsMissingAtDefinition {
  /** The files you want to check */
  files: IFile[];

  /** The prefix used for every Package */
  packagePrefix: string;
}

/**
 * This function check if there is any missing owner at package definition
 *
 * @returns The result of validation for missing owners at definitions
 */
export function checkOwnerIsMissingAtDefinition ({
  files,
  packagePrefix
}: ICheckOwnerIsMissingAtDefinition): boolean {
  let missingOwner = false

  files.forEach((file) => {
    const lines = file.editableContent.split('\n')

    const definitionLines = lines.filter((line) =>
      line.trim().match(/CREATE/i)
    )

    const someOfDefinitionsIsMissingOwner = definitionLines.some(
      (definitionLine) => {
        const packageNameBeginIndex = definitionLine.indexOf(packagePrefix)

        const definitionHasOwner = definitionLine
          .slice(packageNameBeginIndex - 2, packageNameBeginIndex)
          .includes('.')

        return !definitionHasOwner
      }
    )

    someOfDefinitionsIsMissingOwner &&
      file.errors.push('✅ A package não possui owner no Header e/ou Body.')

    if (someOfDefinitionsIsMissingOwner && !missingOwner) missingOwner = true
  })

  return missingOwner
}
