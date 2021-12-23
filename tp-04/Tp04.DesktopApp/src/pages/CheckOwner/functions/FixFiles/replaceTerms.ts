import IFile from '../../interfaces/IFile'
import { SwitchTermPair } from '../../../Settings/entities/SwitchTermPair'

interface IReplaceTermsParams {
  /** files The files you want to replace terms */
  files: IFile[];

  /** switchTermPairs Collection with terms substitution mapping */
  switchTermPairs: SwitchTermPair[];
}

/**
 * This function replaces the `editableContent` property of `IFile`
 * where there is any occurrence of the terms given
 */
export function replaceTerms ({
  files,
  switchTermPairs
}: IReplaceTermsParams): void {
  files.forEach((file: IFile) => {
    switchTermPairs
      .filter(({ newTerm, oldTerm }) => newTerm || oldTerm)
      .forEach(({ oldTerm, newTerm }) => {
        const oldTermRegex = new RegExp(oldTerm, 'gi')

        file.editableContent = file.editableContent.replace(
          oldTermRegex,
          newTerm
        )
      })
  })
}
