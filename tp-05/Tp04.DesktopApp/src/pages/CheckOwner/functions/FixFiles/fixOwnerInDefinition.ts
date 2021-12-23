import { replaceTerms } from './replaceTerms'

import IFile from '../../interfaces/IFile'
import { SwitchTermPair } from '../../../Settings/entities/SwitchTermPair'

interface IFixOwnerInDefinitionParams {
  /** files The files you want to fix */
  files: IFile[];

  /** defaultOwner The default owner to use if its not provided in file */
  defaultOwner: string;

  /** packagePrefix The prefix used for every Package */
  packagePrefix: string;
}

/**
 * This function checks if the package definition contains owner
 * and includes it's default value if not provided
 */
export function fixOwnerInDefinition ({
  files,
  defaultOwner,
  packagePrefix
}: IFixOwnerInDefinitionParams): void {
  files.forEach((file) => {
    const fixedDefinitionTerms: SwitchTermPair[] = []

    const lines = file.editableContent.split('\n')

    const definitionLines = lines.filter((line) =>
      line.trim().match(/CREATE/i)
    )

    definitionLines.forEach((definitionLine) => {
      const packageNameBeginIndex = definitionLine.indexOf(packagePrefix)

      const definitionHasOwner = definitionLine
        .slice(packageNameBeginIndex - 2, packageNameBeginIndex)
        .includes('.')

      if (definitionHasOwner) return

      const definitionWithoutQuotes = definitionLine.replace(/"/gi, '')
      const packageNameBeginWithoutQuotesIndex = definitionWithoutQuotes.indexOf(
        'PG_'
      )

      const endOfPackageNameIndex =
        definitionWithoutQuotes
          .slice(packageNameBeginWithoutQuotesIndex)
          .indexOf(' ') !== -1
          ? definitionWithoutQuotes
            .slice(packageNameBeginWithoutQuotesIndex)
            .indexOf(' ')
          : definitionWithoutQuotes.slice(packageNameBeginWithoutQuotesIndex)
            .length

      const fixedHeaderDefinitionArray: string[] = []

      fixedHeaderDefinitionArray.push(
        definitionWithoutQuotes.slice(0, packageNameBeginWithoutQuotesIndex),
        '"',
        defaultOwner,
        '"."',
        definitionWithoutQuotes.slice(
          packageNameBeginWithoutQuotesIndex,
          packageNameBeginWithoutQuotesIndex + endOfPackageNameIndex
        ),
        '"',
        definitionWithoutQuotes.slice(
          packageNameBeginWithoutQuotesIndex + endOfPackageNameIndex
        )
      )

      fixedDefinitionTerms.push(
        new SwitchTermPair(definitionLine, fixedHeaderDefinitionArray.join(''))
      )
    })

    replaceTerms({ files, switchTermPairs: fixedDefinitionTerms })
  })
}
