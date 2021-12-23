import IFile from '../../interfaces/IFile'

interface ICheckTermPairsParams {
  /** The files you want to check */
  files: IFile[];

  /** Collection with terms substitution mapping */
  termsToCheck: string[];
}

/**
 * This function checks if there is any occurrence of the terms
 * at list and populates `errors` array at type `IFile`
 */
export function checkTermPairs ({
  files,
  termsToCheck
}: ICheckTermPairsParams): void {
  termsToCheck = termsToCheck.filter((term) => !!term.trim())

  files.forEach((file) => {
    const fileContainsTerm = termsToCheck.some((term) =>
      file.editableContent.match(new RegExp(term, 'i'))
    )

    fileContainsTerm &&
      file.errors.push('✅ Contém algum dos termos de troca definidos.')
  })
}
