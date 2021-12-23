import IFile from '../../interfaces/IFile'

interface IShowValidationResultsInConsoleParams {
  /** The files with `errors` property populated */
  files: IFile[];
}

/**
 * This functions prints on console the validation results
 */
export function showValidationResultsInConsole ({
  files
}: IShowValidationResultsInConsoleParams): void {
  const totalOfErrors = files.reduce(
    (accumulator, { errors }) => accumulator + errors.length,
    0
  )

  if (!totalOfErrors) {
    console.log('Sem erros 😉')

    return
  }

  console.log(`Total de erros: ${totalOfErrors}`)
  console.log()

  files.forEach(({ name, errors }) => {
    if (!errors.length) return

    console.log(`Filename: ${name}`)
    errors.forEach((error) => console.log(`${error}`))
    console.log('\n')
  })
}
